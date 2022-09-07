using social_app.Database;
using social_app.Models;
using social_app.Models.Request;

namespace social_app.Repositories
{
    public class PostRepository : IPostRepository
    {

        private readonly SocialAppDbContext _context;

        public PostRepository(IServiceScopeFactory factory)
        {
            /** Resolving scoped instances due to IHostService in PostService.cs */
            _context = factory.CreateScope().ServiceProvider.GetRequiredService<SocialAppDbContext>();
        }

        public void Create(PostRequest request)
        {
            Post post = new Post
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Tag = request.Tag,
                UserId = request.Author
            };

            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void Delete(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
