using ECommerceApplication.Data;
using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ECommerceAuthDbContext db;
        public PaymentRepository(ECommerceAuthDbContext db_)
        {
            db= db_;
        }
        public async Task<PaymentDTO> MakePayment(PaymentMethodDto payment, double amount, string id)
        {
            if (payment.Method.ToLower() == "e-wallet")
            {
                UserWallet wallet=await db.UserWallets.FirstOrDefaultAsync(u=>u.UserId==id && u.Password==payment.Password && u.Amount>=amount);
                if (wallet != null)
                {
                    UsersTransaction t=new UsersTransaction();
                    t.amount = amount;
                    t.UserId= id;
                    t.DateTime = DateTime.Now;
                    wallet.Amount -= amount;
                    try
                    {
                        await db.UsersTransaction.AddAsync(t);
                        await db.SaveChangesAsync();
                        return new PaymentDTO
                        {
                            PaymentMethod = payment.Method,
                            PaymentId = t.Id,
                            PaymentStatus = "Paid",
                            PaymentAmount = amount
                        };
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                    return null;
            }
            else if(payment.Method.ToLower() =="cash on delivery")
            {
                return new PaymentDTO
                {
                    PaymentMethod = payment.Method,
                    PaymentStatus = "Pending",
                    PaymentAmount = amount
                };
            }
            return null;
        }
    }
}
