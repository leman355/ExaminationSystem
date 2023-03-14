using ExaminationSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.ViewModels
{
    public class GroupVM
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Group Name")]
        public string Name { get; set; }
        public string UsersId { get; set; }
        public List<GroupVM> GroupList { get; set; }
        public int TotalCount { get; set; }
        public List<StudentCheckBoxListVM> StudentCheckList { get; set; }
        public GroupVM(Groups model)
        {
            Id = model.Id;
            Name = model.Name ?? "";
            UsersId = model.UsersId.ToString();
        }
        public Groups ConvertViewModel(GroupVM vm)
        {
            return new Groups
            {
                Id = vm.Id,
                Name = vm.Name ?? "",
                UsersId = Guid.Parse(vm.UsersId),
            };
        }
    }
}
