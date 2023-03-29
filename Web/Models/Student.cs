namespace Web.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Contact { get; set; }
        public string? Picture { get; set; }
        public DateTime? StudentCreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
