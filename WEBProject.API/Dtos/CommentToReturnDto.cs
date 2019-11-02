namespace WEBProject.API.Dtos
{
    public class CommentToReturnDto
    {
 
        public UserToReturnDto Author { get; set; }
        public string Text { get; set; }
        public int Grade { get; set; }
    }
}
