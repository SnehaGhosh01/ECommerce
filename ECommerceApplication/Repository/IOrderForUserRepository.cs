using ECommerceApplication.DTO;

namespace ECommerceApplication.Repository
{
    public interface IOrderForUserRepository
    {
        Task<OrderDetailsShowDto> PlaceOrder(PaymentMethodDto payment,string userId);
        Task<List<OrderListsForUserDTO>> ShowAllOrders(string userId);
        Task<List<OrderListsForUserDTO>> ShowExactOrder(string userId, Guid orderId);
        Task<OrderListsForUserDTO> ShowExactOrderInProduct(string userId, Guid orderId, Guid productId);
    }
}
