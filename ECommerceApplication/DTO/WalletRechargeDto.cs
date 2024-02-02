using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class WalletRechargeDto
    {
        [Required]
        [DataType(DataType.CreditCard)]
        public long CardNumber { get; set; }
        [Required]
        public int cvvNumber { get; set; }
        [Required]
        public string CardHolderName { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}
