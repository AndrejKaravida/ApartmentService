namespace WEBProject.API.Dtos
{
    public class CommentToReturnDto
    {
        public int Id { get; set; }
        public UserToReturnDto User { get; set; }
        public string Text { get; set; }
        public int Grade { get; set; }
        public bool Approved { get; set; }
        public bool Deleted { get; set; }
    }
}
