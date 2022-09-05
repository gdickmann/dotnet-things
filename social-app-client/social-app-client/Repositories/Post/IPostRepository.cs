using social_app_client.Models.Post;

namespace social_app_client.Repositories.Post
{
    public interface IPostRepository
    {
        void InsertIntoQueue(PostRequest request);
    }
}
