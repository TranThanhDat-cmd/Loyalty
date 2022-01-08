using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests.Role
{
    public class UpdateRoleRequest
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
