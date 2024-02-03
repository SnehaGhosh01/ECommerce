using ECommerceApplication.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApplication.Models
{
    public class OrderDetailsForUser
    {
        [Key]
        public Guid OrderId { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string ShippingAddress {  get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public double TotalPrice {  get; set; }
        [Required]
        public string PaymentMethod {  get; set; }
       
        public Guid? PaymentId { get; set; }
        
        [Required]
        public string PaymentStatus { get; set; }

        public string OrderItems { get; set; }
        public string PerPrice {  get; set; }

    }
}



