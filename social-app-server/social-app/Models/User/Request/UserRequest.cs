using Microsoft.AspNetCore.Builder;

namespace social_app.Models.User.Request
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
