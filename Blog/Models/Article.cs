namespace Blog.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Id_Tag {  get; set; }
        public int Id_Comment { get; set;}

    }
}
