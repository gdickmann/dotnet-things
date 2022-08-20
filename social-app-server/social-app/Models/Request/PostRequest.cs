namespace social_app.Models.Request
{
    public class PostRequest
    {
        public Guid Author { get; set; }
        public string Title { get; set; }
        public string? Tag { get; set; }
    }
}
