namespace WEBProject.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public Apartment Apartment { get; set; }
        public string Text { get; set; }
        public int Grade { get; set; }
    }
}