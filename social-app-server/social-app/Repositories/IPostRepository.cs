using social_app.Models;
using social_app.Models.Request;

namespace social_app.Repositories
{
    public interface IPostRepository
    {
        void Create(PostRequest request);
        void Update(Post post);
        void Delete(Post post);
        Task<Post> GetById(Guid id);
        Task<List<Post>> GetAll();
    }
}
