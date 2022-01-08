using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests.Role
{
    public class AddRoleRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
