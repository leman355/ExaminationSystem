using Web.Models;

namespace Web.Areas.Dashboard.ViewModels
{
    public class StudentEditVM
    {
        public Student Students { get; set; }
        public List<Group> Groups { get; set; }
        public List<StudentGroup> StudentGroups { get; set; }
    }
}
