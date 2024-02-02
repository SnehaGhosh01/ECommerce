﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.DTO
{
    public class ProfileViewDto
    {
        
        public string Email { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone_Number { get; set; }
    }
}
