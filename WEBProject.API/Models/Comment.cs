namespace WEBProject.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Apartment Apartment { get; set; }
        public string Text { get; set; }
        public int Grade { get; set; }
        public bool Approved { get; set; }
        public bool Deleted { get; set; }
    }
}