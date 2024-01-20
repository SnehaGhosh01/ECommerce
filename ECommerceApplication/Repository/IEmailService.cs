
using System.Threading.Tasks; 
namespace ECommerceApplication.Repository
{
   

    public interface IEmailService
    {
        Task SendRegistrationEmailAsync(string toEmail, string subject, string message);
    }
}
