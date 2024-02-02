using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class UserWalletRegisterDto
    {
        
        
        public string CardNumber {  get; set; }
       
        public string cvvNumber {  get; set; }
       
        public string CardHolderName { get; set; }
        [Required]
        public double Amount {  get; set; }
        [Required]
        public string Password {  get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword {  get; set; }
    }
}
