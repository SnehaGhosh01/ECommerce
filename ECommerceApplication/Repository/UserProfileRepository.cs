using ECommerceApplication.Data;
using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECommerceApplication.Repository
{
    public class UserProfileRepository:IUserProfileRepository
    {
       // private readonly UserManager<ApplicationUser> userManager;
        private readonly ECommerceAuthDbContext db;
        public UserProfileRepository(ECommerceAuthDbContext db_)
        {
            db = db_;
        }
        public async Task<ProfileViewDto> Profile(string userId)
        {
            ApplicationUser user=await db.ApplicationUsers.FindAsync(userId);
            if (user == null)
            {
                return null;
            }
            else
            {
              return new ProfileViewDto
                {
                    Email=user.Email,
                    Name=user.Name,
                    Address=user.Address,
                    Phone_Number = user.PhoneNumber
                };
            }
        }
        public async Task<ProfileViewDto?> UpdateProfile(string id, ProfileEditDto profile)
        {
            var existingProfile = await db.ApplicationUsers.FirstOrDefaultAsync(p=>p.Id==id);
            if (existingProfile == null)
            {
                return null;
            }
            existingProfile.Name = profile.Name;
            existingProfile.Address = profile.Address;
            existingProfile.PhoneNumber = profile.Phone_Number;
            await db.SaveChangesAsync();
            ProfileViewDto p = new ProfileViewDto {
                Phone_Number=existingProfile.PhoneNumber,
                Name=existingProfile.Name,
                Address=existingProfile.Address,
                Email=existingProfile.Email
            };
            return p;
        }

        public async Task<List<TransactionListOfUserDto>> TransactionListOfUser(string userId)
        {
            var transactionsList = await db.UsersTransaction.Where(x => x.UserId == userId).ToListAsync();
            List<TransactionListOfUserDto> ans=new List<TransactionListOfUserDto>();
            foreach (UsersTransaction transaction in transactionsList)
            {
                ans.Add(new TransactionListOfUserDto
                {
                    Id=transaction.Id,
                    amount=transaction.amount,
                    OrderId=transaction.OrderId,
                    DateTime = transaction.DateTime 
                });
            }
            return ans;
        }
    }
}
