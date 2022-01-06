﻿using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
