namespace ExaminationSystem.ViewModels
{
    public class AttendExamVM
    {
        public string StudentId { get; set; }
        public string ExamName { get; set; }
        public List<QnAsVM> QnAs { get; set; }
        public string Message { get; set; }

    }
}
