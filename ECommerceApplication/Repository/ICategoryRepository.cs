using ECommerceApplication.Models;

namespace ECommerceApplication.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(int id,Category category);
        Task<Category?> DeleteAsync(int id);
        Task<Category?> GetByIdAsync(int id);
    }
}
