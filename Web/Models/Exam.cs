namespace Web.Models
{
    public class Exam
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Group Group { get; set; }
        public ExamCategory ExamCategory { get; set; }
    }
}
