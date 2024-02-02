using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class PaymentMethodDto
    {
        public string Method {  get; set; }
        [Required]
        public string Password {  get; set; }
    }
}
