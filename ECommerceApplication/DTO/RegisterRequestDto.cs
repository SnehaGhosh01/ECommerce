using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class VenderRegisterRequestDto
    {
        [Required(ErrorMessage = "Please provide the email address in correct format")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string Phone_Number { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
      
    }
}
