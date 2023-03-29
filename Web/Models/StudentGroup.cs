namespace Web.Models
{
    public class StudentGroup
    {
        public Guid Id { get; set; }
        public Student Student { get; set; }
        public Group Group { get; set; }
    }
}
