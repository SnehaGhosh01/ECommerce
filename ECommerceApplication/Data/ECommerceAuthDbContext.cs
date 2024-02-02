using ECommerceApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ECommerceApplication.Data
{
    public class ECommerceAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public ECommerceAuthDbContext(DbContextOptions<ECommerceAuthDbContext> options) : base(options)
        {
        }
        
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts {  get; set; } 
        public DbSet<OrderDetailsForUser> orderDetailsForUsers { get; set; }
        public DbSet<UsersTransaction> UsersTransaction { get; set; }
        public DbSet<UserWallet> UserWallets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var venderRoleId = "a71a55d6-99d7-4123-b4e0-1218ecb90e3e";
            var userRoleId = "c309fa92-2123-47be-b397-a1c77adb502c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = venderRoleId,
                    ConcurrencyStamp = venderRoleId,
                    Name = "Vender",
                    NormalizedName = "Vender".ToUpper()
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
