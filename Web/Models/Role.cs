using Microsoft.AspNetCore.Identity;

namespace Web.Models
{
    public class Role : IdentityRole
    {
        public string Name { get; set; }
    }
}
