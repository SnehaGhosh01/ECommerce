using ECommerceApplication.Data;
using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;

namespace ECommerceApplication.Repository
{
    public class OrderForUserRepository : IOrderForUserRepository
    {
        private readonly ECommerceAuthDbContext db;
        //private readonly ApplicationUser User;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IPaymentRepository paymentRepository;
        public OrderForUserRepository(ECommerceAuthDbContext db_, IShoppingCartRepository repo, IPaymentRepository paymentRepository)
        {
            db = db_;
            //User = user;
            shoppingCartRepository = repo;
            this.paymentRepository = paymentRepository;

        }
        public async Task<OrderDetailsShowDto> PlaceOrder(PaymentMethodDto payment, string userId)
        {

            List<ShoppingCart> cart = await db.ShoppingCarts.Where(x => x.UserId == userId).ToListAsync();
            ApplicationUser user = db.ApplicationUsers.FirstOrDefault(p => p.Id == userId);
           // var name = "hello";
            List<Product> pro = new List<Product>();

            if (cart.Count > 0)
            {
                double amount = 0;

                foreach (var cartItem in cart)
                {
                    Product protemp = await db.Products.FirstOrDefaultAsync(x => x.ProductId == cartItem.ProductId);
                    pro.Add(protemp);
                    if (protemp.Stock < cartItem.count)
                    {
                        return null;
                    }
                    amount += (cartItem.count * protemp.Price);
                }
                PaymentDTO p = await paymentRepository.MakePayment(payment, amount, userId);
                OrderDetailsForUser order = new OrderDetailsForUser
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    ShippingAddress = payment.Address ,
                    Name= payment.Name ,
                    PhoneNumber=payment.Mobile,
                    TotalPrice = amount,
                    OrderItems = JsonConvert.SerializeObject(cart),
                    PaymentMethod = p.PaymentMethod,
                    PaymentId = p.PaymentId,
                    PaymentStatus = p.PaymentStatus,
                    PerPrice = "null"
                };
                await db.orderDetailsForUsers.AddAsync(order);
                await db.SaveChangesAsync();
                var list = await shoppingCartRepository.GetAllAsync(userId);
                if (order.PaymentId != null)
                {
                    UsersTransaction tu = await db.UsersTransaction.FirstOrDefaultAsync(t => t.Id == p.PaymentId);
                    tu.OrderId = order.OrderId;
                    await db.SaveChangesAsync();
                }
                foreach (var item in cart)
                {
                    Product protemp = await db.Products.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                    protemp.Stock -= item.count;
                    await db.SaveChangesAsync();
                }
                //map to orderdetails to orderdetails show
                OrderDetailsShowDto show = new OrderDetailsShowDto
                {
                    ShoppingCartReturnDto = list,
                    OrderDate = order.OrderDate,
                    OrderId = order.OrderId,
                    PaymentStatus = order.PaymentStatus,

                };
                await shoppingCartRepository.DeleteAllAsync(user.Id);
                return show;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<OrderListsForUserDTO>> ShowAllOrders(string userId)
        {

            var orders = await db.orderDetailsForUsers.Where(x => x.UserId == userId).ToListAsync();

            List<OrderListsForUserDTO> list = new List<OrderListsForUserDTO>();
            foreach (var order in orders)
            {
                List<ShoppingCart> orderItems = JsonConvert.DeserializeObject<List<ShoppingCart>>(order.OrderItems);
                foreach (var item in orderItems)
                {
                    var pro = await db.Products.FindAsync(item.ProductId);
                    var v = await db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == pro.VenderId);
                    list.Add(new OrderListsForUserDTO
                    {
                        ShippingAddress = order.ShippingAddress,
                        Name = order.Name,
                        Mobile = order.PhoneNumber,
                        OrderId = order.OrderId,
                        OrderDate = order.OrderDate,
                        ProductId=pro.ProductId,
                        ProductName = pro.ProductName,
                        Price = pro.Price,
                        totalPrice = item.count * pro.Price,
                        GrandTotal = order.TotalPrice,
                        count = item.count,
                        PaymentStatus = order.PaymentStatus,
                        VenderName = v.Name,
                        ImageUrl = pro.ImageUrl,
                    });
                }
            }
            return list;
        }

        public async Task<List<OrderListsForUserDTO>> ShowExactOrder(string userId, Guid orderId)
        {
            var order = await db.orderDetailsForUsers.FirstOrDefaultAsync(x => x.UserId == userId && x.OrderId == orderId);
            List<ShoppingCart> orderItems = JsonConvert.DeserializeObject<List<ShoppingCart>>(order.OrderItems);
            List<OrderListsForUserDTO> list = new List<OrderListsForUserDTO>();
            foreach (var item in orderItems)
            {
                var pro = await db.Products.FindAsync(item.ProductId);
                var v = await db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == pro.VenderId);
               list.Add( new OrderListsForUserDTO
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ProductName = pro.ProductName,
                    ProductId=pro.ProductId,
                    ShippingAddress=order.ShippingAddress,
                    Name=order.Name,
                    Mobile=order.PhoneNumber,
                    Price = pro.Price,
                    totalPrice = item.count * pro.Price,
                    GrandTotal = order.TotalPrice,
                    count = item.count,
                    PaymentStatus = order.PaymentStatus,
                    VenderName = v.Name,
                    ImageUrl = pro.ImageUrl,
                });
            }
            return list;
        }

        public async Task<OrderListsForUserDTO> ShowExactOrderInProduct(string userId, Guid orderId,Guid productId)
        {
            var order = await db.orderDetailsForUsers.FirstOrDefaultAsync(x => x.UserId == userId && x.OrderId == orderId);
            List<ShoppingCart> orderItems = JsonConvert.DeserializeObject<List<ShoppingCart>>(order.OrderItems);
            // List<OrderListsForUserDTO> list = new List<OrderListsForUserDTO>();
            foreach (var item in orderItems)
            {
                if (item.ProductId == productId)
                {
                    Product pro = await db.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
                    var v = await db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == pro.VenderId);
                    OrderListsForUserDTO exactOrder = new OrderListsForUserDTO
                    {
                        OrderId = order.OrderId,
                        OrderDate = order.OrderDate,
                        ProductName = pro.ProductName,
                        Price = pro.Price,
                        totalPrice = item.count * pro.Price,
                        GrandTotal = order.TotalPrice,
                        count = item.count,
                        PaymentStatus = order.PaymentStatus,
                        VenderName = v.Name,
                        ImageUrl = pro.ImageUrl
                    };
                    return exactOrder;
                }
            }
            return null;
        }

    }
}
