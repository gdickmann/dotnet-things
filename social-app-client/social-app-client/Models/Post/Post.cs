namespace social_app_client.Models.Post
{
    public class Post
    {

        public Post(Guid author, string title, string? tag)
        {
            Id = Guid.NewGuid();
            Author = author;
            Title = title;
            Tag = tag;
        }

        public Guid Id { get; set; }
        public Guid Author { get; set; }
        public string Title { get; set; }
        public string? Tag { get; set; }
    }
}
