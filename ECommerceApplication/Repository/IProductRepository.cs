using ECommerceApplication.DTO;
using ECommerceApplication.Models;
using System.Threading.Tasks;

namespace ECommerceApplication.Repository
{
    public interface IProductRepository
    {
        //Task<List<Product>> SearchByName(string name);
        Task<List<Product>> SearchByProductName(string name);
        Task<List<Product>> SearchByName(string name);
        Task<Product> CreateAsync(ProductCreationDto product);
        Task<Product> DeleteAsync(Guid id);
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task<Product?> UpdateAsync(Guid id, ProductCreationDto product);
        Task<List<Product>> GetSuggestedProduct(string categoryname, string subcategory, int count);

    }
}
