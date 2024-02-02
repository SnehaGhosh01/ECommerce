using ECommerceApplication.DTO;

namespace ECommerceApplication.Repository
{
    public interface IUserWalletRepository
    {
        Task<UserWalletRegisterDto> Registration(UserWalletRegisterDto r, string userid);
        Task<string> RechargeWallet(WalletRechargeDto r, string userid);
        Task<string> ChangePasswordForWallet(PasswordChangeDto pass, string userId);
        Task<string> ForgetPassword(ForgetPasswordDto dto, string userId);
        Task<string> CheckBalance(string userId);
    }
}
