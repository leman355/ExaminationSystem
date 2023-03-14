using Microsoft.AspNetCore.Identity;

namespace ExaminationSystem.Models
{
    public class Users: IdentityUser
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<Groups> Groups { get; set; } = new HashSet<Groups>();
    }
}
