using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class PaymentMethodDto
    {
        public string Method {  get; set; }
        public string Password {  get; set; }
        public string Address { get; set; }
        public string Name {  get; set; }
        public string Mobile {  get; set; }

    }
}
