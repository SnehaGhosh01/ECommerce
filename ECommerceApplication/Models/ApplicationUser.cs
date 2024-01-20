﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name {  get; set; }
        [Required]

        public string Address { get; set; }
        public string? VenderLicenceNumber { get; set; }

    }
}
