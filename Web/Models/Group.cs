namespace Web.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public DateTime GroupCreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
