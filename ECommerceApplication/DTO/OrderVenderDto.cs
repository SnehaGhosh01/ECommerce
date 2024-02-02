using ECommerceApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class OrderVenderDto
    {
        public Guid OrderId { get; set; }
        public DateTime OrderShippingLastDate { get; set; }
        public string ShippingAddress { get; set; }
        public double TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderItems { get; set; }
        public string PerPrice { get; set; }
    }
}
