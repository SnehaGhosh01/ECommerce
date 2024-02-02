using ECommerceApplication.Data;
using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Repository
{
    public class UserWalletRepository:IUserWalletRepository
    {
        private readonly ECommerceAuthDbContext db;
        public UserWalletRepository(ECommerceAuthDbContext db_) {
            db = db_;
        }
         public async Task<UserWalletRegisterDto> Registration(UserWalletRegisterDto r,string userid)
        {
            var user=await db.UserWallets.FirstOrDefaultAsync(w=>w.UserId==userid);
            if (user == null)
            {
                UserWallet wallet = new UserWallet
                {
                    UserId = userid,
                    Password = r.Password,
                    ConfirmPassword = r.ConfirmPassword,
                    Amount = r.Amount,
                };
                await db.UserWallets.AddAsync(wallet);
                await db.SaveChangesAsync();
                return r;
            }
            return null;
        }
        public async Task<string> RechargeWallet(WalletRechargeDto r, string userid)
        {
            var wallet=await db.UserWallets.FirstOrDefaultAsync(x=>x.UserId==userid);
            if(wallet == null)
            {
                return "You don't have e-wallet. U need to register it...";
            }
            else
            {
                wallet.Amount += r.Amount;
                await db.SaveChangesAsync();
                return "Successfully Recharged. Happy Shopping";
            }
        }
        public async Task<string> ChangePasswordForWallet(PasswordChangeDto pass,string userId)
        {
            var user= await db.UserWallets.FirstOrDefaultAsync(w=>w.UserId==userId && w.Password==pass.CurrentPassword);
            if (user == null)
            {
                return "You don't have account. Register First";
            }
            if(pass.CurrentPassword==pass.NewPassword)
            {
                return "Your Password is same as old";
            }
            user.Password = pass.NewPassword;
            user.ConfirmPassword = pass.ConfirmPassword;
            await db.SaveChangesAsync();
            return "Suceessfully chanaged your password";

        }
        public async Task<string> ForgetPassword(ForgetPasswordDto dto,string userId)
        {
            //bool identify=false;
            var user = await db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == userId);
            if(user == null)
            {
                return "Something went wrong";
            }
            var wallet=await db.UserWallets.FirstOrDefaultAsync(x=>x.UserId==userId);
            if (wallet == null)
            {
                return "Something went wrong.Register e-wallet";
            }
            if (dto.Email == user.Email)
            {
                wallet.Password = dto.NewPassword;
                wallet.ConfirmPassword = dto.ConfirmPassword;
                await db.SaveChangesAsync();
                return "Password has been set";
            }
            else
            {
                return "Wrong Email.Try Again..";
            }

        }

        public async Task<string> CheckBalance(string userId)
        {
            UserWallet wallet=await db.UserWallets.FirstOrDefaultAsync(x=>x.UserId== userId);
            if(wallet == null)
            {
                return "Sorry but you don't have any e-wallet";
            }
            else
            {
                return $"Your Wallet Balance is: {wallet.Amount}";
            }
        }

    }
}
