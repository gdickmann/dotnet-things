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
            User user = new(request.Name, request.Email, request.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            _logger.LogInformation($"User {user.Id} successfully created via gRPC", DateTime.UtcNow.ToLongTimeString());

            return Task.FromResult(new EmptyGrpc());
        }

        public override Task<EmptyGrpc> Update(UpdateUser request, ServerCallContext context)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id.ToString() == request.Id);

            if (user != null)
            {
                User updatedUser = new(request.Name, request.Email, request.Password);
                updatedUser.Id = Guid.Parse(request.Id);

                _context.Users.Update(updatedUser);
                _context.SaveChanges();

                _logger.LogInformation($"User {request.Id} successfully updated via gRPC", DateTime.UtcNow.ToLongTimeString());
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

                _logger.LogInformation($"User {request.Id} successfully deleted via gRPC", DateTime.UtcNow.ToLongTimeString());
            }

            return Task.FromResult(new EmptyGrpc());
        }

        public override Task<UserGrpc> Get(UserIdGrpc request, ServerCallContext context)
        {
            var response = _context.Users.Find(Guid.Parse(request.Id));

            _logger.LogInformation($"Get request from user {request.Id} made via gRPC", DateTime.UtcNow.ToLongTimeString());

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

            _logger.LogInformation("Get all request made via gRPC", DateTime.UtcNow.ToLongTimeString());

            return Task.FromResult(response);
        }

    }
}
