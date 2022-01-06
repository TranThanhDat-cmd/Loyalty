using Microsoft.AspNetCore.Identity;
namespace Loyalty.Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
