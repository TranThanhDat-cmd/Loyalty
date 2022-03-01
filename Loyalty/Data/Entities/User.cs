using Microsoft.AspNetCore.Identity;

namespace Loyalty.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }


    }
}
