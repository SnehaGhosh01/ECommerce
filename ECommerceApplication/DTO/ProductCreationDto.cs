using ECommerceApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class ProductCreationDto
    {
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
        public string VenderEmail {  get; set; }
        [Required]
        public string CategoryName { get; set;}
    }
}
