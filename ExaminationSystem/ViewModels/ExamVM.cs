using ExaminationSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ExaminationSystem.ViewModels
{
    public class ExamVM
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Exam Name")]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public string GroupsId { get; set; }
        public List<ExamVM> ExamList { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<Groups> GroupList { get; set; }
        public ExamVM(Exams model)
        {
            Id = model.Id;
            Title = model.Title ?? "";
            StartDate = model.StartDate;
            Time = model.Time;
            GroupsId = model.GroupsId.ToString();
        }
        public Exams ConvertViewModel(ExamVM vm)
        {
            return new Exams
            {
                Id = vm.Id,
                Title = vm.Title ?? "",
                StartDate = vm.StartDate,
                Time = vm.Time,
                GroupsId = Guid.Parse(vm.GroupsId),
            };
        }
    }
}
