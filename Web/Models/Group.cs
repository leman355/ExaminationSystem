namespace Web.Models
{
    public class Group
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public DateTime GroupCreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
