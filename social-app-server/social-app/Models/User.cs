using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_app.Models
{
    public class User
    {

        public User(string username, string email, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(38)]
        public string Username { get; set; }

        [Required]
        [MaxLength(254)]
        public string Email { get; set; }

        [Required]
        /** "But passwords shouldn't have a maximum length!" https://xkcd.com/936/ */
        [MaxLength(64)]
        public string Password { get; set; }

        public ICollection<Post>? Posts { get; set; }
    }
}
