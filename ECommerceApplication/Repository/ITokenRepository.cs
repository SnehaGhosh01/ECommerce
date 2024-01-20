using ECommerceApplication.Models;

namespace ECommerceApplication.Repository
{
    public interface ITokenRepository
    {
       string CreateJWTToken(ApplicationUser user, List<string> roles);
    }
}
