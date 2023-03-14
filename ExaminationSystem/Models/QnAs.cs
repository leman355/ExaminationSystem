namespace ExaminationSystem.Models
{
    public class QnAs
    {
        public Guid Id { get; set; }
        public Guid ExamsId { get; set; }
        public Exams Exams { get; set; }
        public string Question { get; set; }
        public int Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public ICollection<ExamResults> ExamResults { get; set; }
    }
}
