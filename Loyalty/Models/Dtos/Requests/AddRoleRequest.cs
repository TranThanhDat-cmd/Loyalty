using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests
{
    public class AddRoleRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
