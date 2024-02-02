namespace ECommerceApplication.DTO
{
    public class OrderDetailsShowDto
    {
        public List<ShoppingCartReturnDto> ShoppingCartReturnDto { get; set;}
        public DateTime OrderDate { get; set;}
        public Guid OrderId { get; set;}
        public string PaymentStatus {  get; set;}

    }
}
