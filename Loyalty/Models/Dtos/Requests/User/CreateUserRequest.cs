using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests.User
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
        public string RoleName { get; set; }


    }
}
