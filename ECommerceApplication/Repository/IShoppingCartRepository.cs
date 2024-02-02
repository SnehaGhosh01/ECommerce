using ECommerceApplication.DTO;
using ECommerceApplication.Models;

namespace ECommerceApplication.Repository
{
    public interface IShoppingCartRepository
    {
        Task<List<ShoppingCartReturnDto>> AddAsync(Guid id, string userId);
        Task<List<ShoppingCartReturnDto>> DeleteAsync(Guid id, string userId);
        Task<List<ShoppingCartReturnDto>> GetAllAsync(string id);
        Task<ShoppingCartReturnDto> GetByIdAsync(Guid id, string userId);
        Task<ShoppingCartReturnDto> UpdateCountPlusAsync(Guid id,string userId);
        Task<ShoppingCartReturnDto> UpdateCountMinusAsync(Guid id, string userId);
        Task<List<ShoppingCartReturnDto>> DeleteAllAsync(string userId);
    }
}
