using social_app.Database;
using social_app.Models;
using social_app.Models.Request;
using System.Data.Entity;

namespace social_app.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly SocialAppDbContext _context;

        public UserRepository(SocialAppDbContext context)
        {
            _context = context;
        }

        public void Create(UserRequest user)
        {
            _context.Add(new User
            {
                Username = user.Name,
                Password = user.Password,
                Email = user.Email,
            });
        }

        public void Update(User user)
        {
            _context.Entry(user).CurrentValues.SetValues(user);
        }

        public void Delete(User user)
        {
            _context.Remove(user);
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
