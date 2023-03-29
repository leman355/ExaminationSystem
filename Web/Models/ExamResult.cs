namespace Web.Models
{
    public class ExamResult
    {
        public Guid Id { get; set; }
        public Exam Exam { get; set; }
        public User User { get; set; }
    }
}
