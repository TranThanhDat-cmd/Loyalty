using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests.Auth
{
    public class UserRegisterRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
