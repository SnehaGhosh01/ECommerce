using ECommerceApplication.DTO;

namespace ECommerceApplication.Repository
{
    public interface IPaymentRepository
    {
        Task<PaymentDTO> MakePayment(PaymentMethodDto payment, double amount,string id);
    }
}
