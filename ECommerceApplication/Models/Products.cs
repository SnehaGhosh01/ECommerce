using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Price")]
        // [DataType(DataType.Currency )]
        //[Range(1, 1000)]
        public double Price { get; set; }
        [Required]
        public long Stock { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        // [ValidateNever]
        public Category Category { get; set; }
        [Required]
        public string VenderId { get; set; }
        [ForeignKey("VenderId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }





    }
}
