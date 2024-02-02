using ECommerceApplication.Data;
using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace ECommerceApplication.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ECommerceAuthDbContext db;
       // private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
      //  private readonly string userId;
        public ShoppingCartRepository(ECommerceAuthDbContext _context, UserManager<ApplicationUser> userManager_, IHttpContextAccessor httpContextAccessor)
        {
            db = _context;
            userManager = userManager_;
            //this.httpContextAccessor = httpContextAccessor;
           //userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        
        public async Task<List<ShoppingCartReturnDto>> AddAsync(Guid id,string userId)
        {
            var pro = db.Products.FirstOrDefault(c => c.ProductId == id);
            if (pro == null)
            {
                return null;
            }
            ShoppingCart alreadyincart=await db.ShoppingCarts.FirstOrDefaultAsync(c => c.ProductId == id && c.UserId==userId);
            if (alreadyincart != null)
            {
                return await GetAllAsync(userId);
            }
            else
            {
                ShoppingCart newAdd = new ShoppingCart
                {
                    ProductId = id,
                    UserId = userId,
                    count = 1
                };
                db.ShoppingCarts.Add(newAdd);
            }
            await db.SaveChangesAsync(); // Use asynchronous SaveChanges
            var v = await userManager.FindByIdAsync(pro.VenderId);
            if (v == null)
            {
                return null;
            }
            //ShoppingCartReturnDto shopping = new ShoppingCartReturnDto
            //{
            //ImageUrl = pro.ImageUrl,
            //ProductName = pro.ProductName,
            //VenderName = v.Name,
            //Price = pro.Price,
            // count = 1,
            //totalPrice = (pro.Price)
            // };
            // return shopping;
            return await GetAllAsync(userId);

        }


        public async Task<List<ShoppingCartReturnDto>> DeleteAsync(Guid id,string userId)
        {
            ShoppingCart cart= await db.ShoppingCarts.FirstOrDefaultAsync(c=>c.ProductId== id && c.UserId==userId);
            var name = "lol";
            if (cart != null)
            {
                db.Remove(cart);
                await db.SaveChangesAsync();
            }
            else { return null;  }
            return await GetAllAsync(userId);
        }
        
        public async Task<List<ShoppingCartReturnDto>> GetAllAsync(string id)
        {
            //string userId = User.Identity.GetUserId();

            
            var cartdetails = await db.ShoppingCarts.Where(s => s.UserId == id).ToListAsync();
            var name = "Lol";
            List<ShoppingCartReturnDto> res =new List<ShoppingCartReturnDto>();
            if (cartdetails == null)
            {
                return null;
            }
            foreach (var cart in cartdetails)
            {
                Product pro=db.Products.FirstOrDefault(p => p.ProductId == cart.ProductId);
                ApplicationUser v = db.ApplicationUsers.FirstOrDefault(p => p.Id == pro.VenderId);
                var n = "name";
                res.Add(new ShoppingCartReturnDto
                {
                    ImageUrl = pro.ImageUrl,
                    ProductName =pro.ProductName,
                    VenderName = v.Name,
                    Price =pro.Price,
                    count=cart.count,
                    totalPrice= (pro.Price)*(cart.count)
                });
            }
            return res;
        }

        public async Task<ShoppingCartReturnDto> GetByIdAsync(Guid id, string userId)
        {
            ShoppingCart cartdetails = await db.ShoppingCarts.FirstOrDefaultAsync(s => s.UserId == userId && s.ProductId==id);
            if (cartdetails == null)
            {
                return null;
            }
             Product pro = db.Products.FirstOrDefault(p => p.ProductId == cartdetails.ProductId);
                ApplicationUser v = db.ApplicationUsers.FirstOrDefault(p => p.Id == pro.VenderId);
                var n = "name";
               ShoppingCartReturnDto shop= new ShoppingCartReturnDto
                {
                    ImageUrl = pro.ImageUrl,
                    ProductName = pro.ProductName,
                    VenderName = v.Name,
                    Price = pro.Price,
                    count = cartdetails.count,
                    totalPrice = (pro.Price) * (cartdetails.count)
                };
            
            return shop;
        }

        public async Task<ShoppingCartReturnDto> UpdateCountPlusAsync(Guid id,string userId)
        {
            int newCount = 1;
            ShoppingCart existingCartItem = await db.ShoppingCarts.FirstOrDefaultAsync(c => c.ProductId == id && c.UserId == userId);

            if (existingCartItem == null)
            {
                // Item not found in the cart.
                return null;
            }

            // Update the count.
            existingCartItem.count += newCount;

            // Save changes to the database.
            await db.SaveChangesAsync();
            var lol = "lol";
            Product pro=await db.Products.FirstOrDefaultAsync(p=>p.ProductId==existingCartItem.ProductId);
            var hello = "hello";
            ApplicationUser v = db.ApplicationUsers.FirstOrDefault(p => p.Id == pro.VenderId);
            var name = "lol";
            // Return the updated shopping cart item.
            var updatedCartItem = new ShoppingCartReturnDto
            {
                ProductName =pro.ProductName,
                Price = pro.Price,
                ImageUrl = pro.ImageUrl,
                count = existingCartItem.count,
                VenderName = v.Name,
                totalPrice = pro.Price * existingCartItem.count
            };

            return updatedCartItem;
        }
        public async Task<ShoppingCartReturnDto> UpdateCountMinusAsync(Guid id, string userId)
        {
            int newCount = 1;
            ShoppingCart existingCartItem = await db.ShoppingCarts.FirstOrDefaultAsync(c => c.ProductId == id && c.UserId == userId);

            if (existingCartItem == null)
            {
                // Item not found in the cart.
                return null;
            }

            // Update the count.
            existingCartItem.count -= newCount;

            // Save changes to the database.
            await db.SaveChangesAsync();

            var lol = "lol";
            Product pro = await db.Products.FirstOrDefaultAsync(p => p.ProductId == existingCartItem.ProductId);
            var hello = "hello";
            ApplicationUser v = db.ApplicationUsers.FirstOrDefault(p => p.Id == pro.VenderId);
            var name = "lol";
            // Return the updated shopping cart item.
            var updatedCartItem = new ShoppingCartReturnDto
            {
                ProductName = pro.ProductName,
                Price = pro.Price,
                ImageUrl = pro.ImageUrl,
                count = existingCartItem.count,
                VenderName = v.Name,
                totalPrice = pro.Price * existingCartItem.count
            };

            return updatedCartItem;
        }

        public async Task<List<ShoppingCartReturnDto>> DeleteAllAsync(string userId)
        {
            var cartdetails = await db.ShoppingCarts.Where(s => s.UserId == userId).ToListAsync();
            db.ShoppingCarts.RemoveRange(cartdetails);
            await db.SaveChangesAsync();
            return await GetAllAsync(userId);
        }
    }
}
