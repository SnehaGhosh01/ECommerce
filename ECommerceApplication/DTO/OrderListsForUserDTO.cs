using System.Text.Json.Serialization;

namespace ECommerceApplication.DTO
{
    public class OrderListsForUserDTO
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }

        public string ImageUrl { get; set; }
        //[JsonIgnore]

        public string VenderName { get; set; }

        public int count { get; set; }
        public double totalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentStatus { get; set; }
        public double GrandTotal {  get; set; }
    }
}
