using Microsoft.EntityFrameworkCore;

namespace Web.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
    }
}
