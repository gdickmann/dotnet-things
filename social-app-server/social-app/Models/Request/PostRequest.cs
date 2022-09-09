namespace social_app.Models.Request
{
    public class PostRequest
    {

        public PostRequest(Guid author, string title)
        {
            AuthorId = author;
            Title = title;
        }

        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string? Tag { get; set; }
    }
}
