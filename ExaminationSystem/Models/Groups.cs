namespace ExaminationSystem.Models
{
    public class Groups
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UsersId { get; set; }
        public Users Users { get; set; }
        public ICollection<Students> Students { get; set; } = new HashSet<Students>();
        public ICollection<Exams> Exams { get; set; } = new HashSet<Exams>();
    }
}
