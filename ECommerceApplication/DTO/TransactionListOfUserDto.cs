using ECommerceApplication.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class TransactionListOfUserDto
    {
        public Guid Id { get; set; }
        public double amount { get; set; }
        public Guid? OrderId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
