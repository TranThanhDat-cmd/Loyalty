using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests.User
{
    public class AddRoleUserRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
