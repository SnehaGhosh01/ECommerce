using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApplication.Models
{
    public class UsersTransaction
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public double amount {  get; set; }
        public Guid? OrderId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}
