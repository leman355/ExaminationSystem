namespace ExaminationSystem.Models
{
    public class Exams
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public Guid GroupsId { get; set; }
        public Groups Groups { get; set; }
        public ICollection<ExamResults> ExamResults { get; set; } = new HashSet<ExamResults>();
        public ICollection<QnAs> QnAs{ get; set; } = new HashSet<QnAs>();
    }
}
