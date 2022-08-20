using social_app.Database;
using social_app.Models;
using social_app.Models.Request;
using System.Data.Entity;

namespace social_app.Repositories
{
    public class PostRepository : IPostRepository
    {

        private readonly SocialAppDbContext _context;
        private readonly DbSet<Post> _dbSet;

        public PostRepository(SocialAppDbContext context, DbSet<Post> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public void Create(PostRequest request)
        {
            var author = _context.Users.Find(request.Author);

            _context.Posts.Add(new Post
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Tag = request.Tag,
                User = author
            });
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
