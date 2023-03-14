using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.ViewModels
{
    public class LoginVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
