namespace ExaminationSystem.Models
{
    public class Students
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Contact { get; set; }
        public string? Picture { get; set; }
        public Guid? GroupId { get; set; }
        public Groups Groups { get; set; }
        public ICollection<ExamResults> ExamResults { get; set; } = new HashSet<ExamResults>();
    }
}
