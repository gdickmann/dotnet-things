using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_app.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid Author { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
    }
}