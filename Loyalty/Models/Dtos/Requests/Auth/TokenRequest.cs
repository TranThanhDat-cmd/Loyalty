using System.ComponentModel.DataAnnotations;

namespace Loyalty.Models.Dtos.Requests.Auth
{
    public class TokenRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
