using Microsoft.AspNetCore.Identity;

namespace Web.Models
{
    public class Role:IdentityRole
    {
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
    }
}
