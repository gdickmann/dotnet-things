using Grpc.Core;
using social_app.Models;
using social_app.Repositories.User;

namespace social_app.gRPC.Services
{
    public class UserService : UserStream.UserStreamBase
    {

        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _repository;

        public UserService(ILogger<UserService> logger, IUserRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public override Task<EmptyGrpc> Create(UserGrpc request, ServerCallContext context)
        {
            User user = new(request.Name, request.Email, request.Password);

            _repository.Create(user);
            _logger.LogInformation($"User {user.Id} successfully created via gRPC", DateTime.UtcNow.ToLongTimeString());

            return Task.FromResult(new EmptyGrpc());
        }

        public override Task<EmptyGrpc> Update(UpdateUser request, ServerCallContext context)
        {
            var user = _repository.Get(Guid.Parse(request.Id));

            if (user != null)
            {
                User updatedUser = new(request.Name, request.Email, request.Password);
                updatedUser.Id = Guid.Parse(request.Id);

                _repository.Update(updatedUser);
                _logger.LogInformation($"User {request.Id} successfully updated via gRPC", DateTime.UtcNow.ToLongTimeString());
            }

            return Task.FromResult(new EmptyGrpc());
        }

        public override Task<EmptyGrpc> Delete(UserIdGrpc request, ServerCallContext context)
        {
            var user = _repository.Get(Guid.Parse(request.Id));

            if (user != null)
            {
                _repository.Delete(user);
                _logger.LogInformation($"User {request.Id} successfully deleted via gRPC", DateTime.UtcNow.ToLongTimeString());
            }

            return Task.FromResult(new EmptyGrpc());
        }

        public override Task<UserGrpc> Get(UserIdGrpc request, ServerCallContext context)
        {
            var response = _repository.Get(Guid.Parse(request.Id));
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
            UsersGrpc response = _repository.GetAll();
            _logger.LogInformation("Get all request made via gRPC", DateTime.UtcNow.ToLongTimeString());

            return Task.FromResult(response);
        }
    }
}
