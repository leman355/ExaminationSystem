namespace Web.Models
{
    public class QuestionAnswer
    {
        public Guid Id { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
