using ECommerceApplication.Data;
using ECommerceApplication.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceAuthDbContext db;
        public CategoryRepository(ECommerceAuthDbContext db)
        {
            this.db = db;
        }
        public async Task<Category> CreateAsync(Category category)
        {
           // category.Name=category.Name.ToUpper();
            await db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
            return category;
            
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var existingCategory= await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCategory == null) {
                return null;
            }
            db.Categories.Remove(existingCategory);
            await db.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<List<Category>> GetAllAsync()
        {
           var list= await db.Categories.ToListAsync();
            return list;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Category?> UpdateAsync(int id, Category category)
        {
            var existingCategory= await db.Categories.FirstOrDefaultAsync(x=>x.Id == id);
            if(existingCategory == null) { 
                return null;
            }
            existingCategory.Name = category.Name;

            await db.SaveChangesAsync();
            return existingCategory;
        }
    }
}
