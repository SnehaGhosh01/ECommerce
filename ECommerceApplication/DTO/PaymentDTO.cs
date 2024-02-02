namespace ECommerceApplication.DTO
{
    public class PaymentDTO
    {
        public Guid? PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod {  get; set; }
        public double PaymentAmount { get; set; }

        
    }
}
