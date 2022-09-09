using social_app.Models;
using social_app.Models.Request;

namespace social_app.Repositories.Post
{
    public interface IPostRepository
    {
        void Create(PostRequest request);
    }
}
