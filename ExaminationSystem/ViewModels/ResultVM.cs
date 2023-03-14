
using ExaminationSystem.Models;

namespace ExaminationSystem.ViewModels
{
    public class ResultVM
    {
        public List<ExamResults> ExamResults { get; set; }
        public List<Students> Student { get; set; }
        public List<Groups> Group { get; set; }

        //public string StudentId { get; set; }
        //public string ExamName { get; set; }
        //public int TotalQuestion { get; set; }
        //public int CorrectAnswer { get; set; }
        //public int WrongAnswer { get; set; }
    }
}
