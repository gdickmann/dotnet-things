using social_app.Database;
using social_app.Models.Request;

namespace social_app.Repositories.Post
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
            Models.Post post = new(request.Title, request.Tag, request.AuthorId);

            _context.Posts.Add(post);
            _context.SaveChanges();

            _logger.LogInformation($"Post {post.Id} inserted into database", DateTime.UtcNow.ToLongTimeString());
        }
    }
}
