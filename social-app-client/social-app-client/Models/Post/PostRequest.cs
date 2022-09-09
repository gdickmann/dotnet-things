using System.ComponentModel.DataAnnotations;

namespace social_app_client.Models.Post
{
    public class PostRequest
    {

        public PostRequest(Guid author, string title, string? tag)
        {
            Author = author;
            Title = title;
            Tag = tag;
        }

        public Guid Author { get; set; }
        public string Title { get; set; }
        public string? Tag { get; set; }
    }
}
