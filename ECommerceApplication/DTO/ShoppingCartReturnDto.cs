using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceApplication.DTO
{
    public class ShoppingCartReturnDto
    {
        
        
        public string ProductName { get; set; }       
        public double Price { get; set; }
        
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
        //[JsonIgnore]

        public string VenderName { get; set; }
        
        public int count {  get; set; }
        public double totalPrice {  get; set; }
    }
}
