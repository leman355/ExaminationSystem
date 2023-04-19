using Web.Models;

namespace Web.Areas.Dashboard.ViewModels
{
    public class QuestionEditVM
    {
        public Question Questions { get; set; }
        public List<Answer> Answers { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
