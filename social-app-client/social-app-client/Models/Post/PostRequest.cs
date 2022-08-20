namespace social_app_client.Models.Post
{
    public class PostRequest
    {
        public Guid Author { get; set; }
        public string Title { get; set; }
        public string? Tag { get; set; }
    }
}
