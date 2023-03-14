namespace ExaminationSystem.Models
{
    public class ExamResults
    {
        public Guid Id { get; set; }
        public Guid? StudentsId { get; set; }
        public Students Students { get; set; }
        public Guid? ExamsId { get; set; }
        public Exams Exams { get; set; }
        public int Answer { get; set; }
        public Guid? QnAsId { get; set; }
        public QnAs QnAs { get; set; }

    }
}
