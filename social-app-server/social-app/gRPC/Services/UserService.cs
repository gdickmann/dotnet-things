using Grpc.Core;
using social_app.Database;
using social_app.Models;

namespace social_app.gRPC.Services
{
    public class UserService : UserStream.UserStreamBase
    {

        private readonly ILogger<UserService> _logger;
        private SocialAppDbContext _context;

        public UserService(ILogger<UserService> logger, SocialAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override Task<EmptyGrpc> Create(UserGrpc request, ServerCallContext context)
        {
            _context.Users.Add(new User 
            { 
                Id = Guid.NewGuid(),
                Username = request.Name,
                Email = request.Email,
                Password = request.Password 
            });
            _context.SaveChanges();

            _logger.LogInformation("User successfully created via gRPC", DateTime.UtcNow);

            return Task.FromResult(new EmptyGrpc());
        }

        public override Task<EmptyGrpc> Update(UpdateUser request, ServerCallContext context)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == request.Id);

            if (user != null)
            {
                _context.Users.Update(new User
                { 
                    Id = Guid.Parse(request.Id),
                    Username = request.Name,
                    Email = request.Email,
                    Password = request.Password 
                });
                _context.SaveChanges();
            }

            return Task.FromResult(new EmptyGrpc());
        }

        public override Task<EmptyGrpc> Delete(UserIdGrpc request, ServerCallContext context)
        {
            var user = _context.Users.Find(Guid.Parse(request.Id));

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }

            return Task.FromResult(new EmptyGrpc());
        }

        public override Task<UserGrpc> Get(UserIdGrpc request, ServerCallContext context)
        {
            var response = _context.Users.Find(Guid.Parse(request.Id));

            return Task.FromResult(new UserGrpc
            { 
                Name = response?.Username,
                Email = response?.Email,
                Password = response?.Password
            });
        }

        public override Task<UsersGrpc> GetAll(EmptyGrpc request, ServerCallContext context)
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

            return Task.FromResult(response);
        }

    }
}
