using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests
{
    public class CreateUserRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public List<string> RoleNames { get; set; }
    }
}
