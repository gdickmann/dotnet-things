using social_app.Database;

namespace social_app.Repositories.User
{
    public class UserRepository : IUserRepository
    {

        private readonly SocialAppDbContext _context;

        public UserRepository(SocialAppDbContext context)
        {
            _context = context;
        }

        public void Create(Models.User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public void Delete(Models.User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public Models.User? Get(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public UsersGrpc GetAll()
        {
            UsersGrpc response = new UsersGrpc();

            var query = from user in _context.Users
                        select new UpdateUser()
                        {
                            Id = user.Id.ToString(),
                            Name = user.Username,
                            Email = user.Email,
                            Password = user.Password
                        };
            response.Users.AddRange(query.ToArray());

            return response;
        }

        public void Update(Models.User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
