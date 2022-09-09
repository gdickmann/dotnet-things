using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_app.Models
{
    public class Post
    {

        public Post(string title, string? tag, Guid userId)
        {
            Id = Guid.NewGuid();
            Title = title;
            Tag = tag;
            UserId = userId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(15)]
        public string? Tag { get; set; }
    }
}