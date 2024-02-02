using ECommerceApplication.DTO;

namespace ECommerceApplication.Repository
{
    public interface IUserProfileRepository
    {
        Task<ProfileViewDto> Profile(string userId);
        Task<ProfileViewDto?> UpdateProfile(string id, ProfileEditDto profile);
        Task<List<TransactionListOfUserDto>> TransactionListOfUser(string userId);
    }
}
