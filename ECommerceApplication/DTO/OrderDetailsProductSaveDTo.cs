using ECommerceApplication.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class OrderDetailsProductSaveDTo
    {
        [Key]
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public double CurrentPriceOfProduct {  get; set; }
        

    }
}
