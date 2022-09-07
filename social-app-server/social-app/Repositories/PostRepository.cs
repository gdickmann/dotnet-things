using social_app.Database;
using social_app.Models;
using social_app.Models.Request;

namespace social_app.Repositories
{
    public class PostRepository : IPostRepository
    {

        private readonly SocialAppDbContext _context;
        private readonly ILogger<PostRepository> _logger;

        public PostRepository(IServiceScopeFactory factory, ILogger<PostRepository> logger)
        {
            /** Resolving scoped instances due to IHostService in PostService.cs */
            _context = factory.CreateScope().ServiceProvider.GetRequiredService<SocialAppDbContext>();
            _logger = logger;
        }

        public void Create(PostRequest request)
        {
            Post post = new()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Tag = request.Tag,
                UserId = request.Author
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            _logger.LogInformation("Post inserted into database", DateTime.UtcNow.ToLongTimeString());
        }

        public void Delete(Post post)
        {
            _context.Remove(post);
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
