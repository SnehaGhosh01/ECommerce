using System.Text.Json.Serialization;

namespace ECommerceApplication.DTO
{
    public class OrderListsForUserDTO
    {
        public Guid OrderId { get; set; }
        
        public string ProductName { get; set; }
        public string ShippingAddress {  get; set; }
       
        
        public Guid ProductId { get; set; }
        public double Price { get; set; }

        public string ImageUrl { get; set; }
        //[JsonIgnore]
        public string Name { get; set; }

        public string VenderName { get; set; }

        public int count { get; set; }
        public double totalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentStatus { get; set; }
        public double GrandTotal {  get; set; }
        public string Mobile { get; set; }
    }
}
