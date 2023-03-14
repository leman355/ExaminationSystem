using ExaminationSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ExaminationSystem.ViewModels
{
    public class StudentVM
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Contact No")]
        public string? Contact { get; set; }
        public string? Picture { get; set; }
        public string? GroupId { get; set; }
        public int TotalCount { get; set; }
        public List<StudentVM> StudentList { get; set; }
        public StudentVM(Students model)
        {
            Id = model.Id;
            Name = model.Name ?? "";
            UserName = model.UserName;
            Password = model.Password;
            Contact = model.Contact ?? "";
            Picture = model.Picture;
            GroupId= model.GroupId.ToString();
        }
        public Students ConvertViewModel(StudentVM vm)
        {
            return new Students
            {
                Id = vm.Id,
                Name = vm.Name ?? "",
                UserName = vm.UserName,
                Password = vm.Password,
                Contact = vm.Contact ?? "",
                Picture = vm.Picture,
                GroupId = Guid.Parse(vm.GroupId),
            };
        }
    }
}
