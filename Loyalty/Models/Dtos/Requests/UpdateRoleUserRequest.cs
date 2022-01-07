﻿using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests
{
    public class UpdateRoleUserRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string OldRoleName { get; set; }
        [Required]
        public string NewRoleName { get; set; }
    }
}
