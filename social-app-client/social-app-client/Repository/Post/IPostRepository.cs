using social_app_client.Models.Post;

namespace social_app_client.Repository.Post
{
    public interface IPostRepository
    {

        public void InsertIntoQueue(PostRequest request);

    }
}
