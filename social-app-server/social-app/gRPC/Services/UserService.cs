using Grpc.Core;
using social_app.Database;
using social_app.Models.User;

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

            _context.Users.Add(new User { Id = Guid.NewGuid(), Username = request.Name, Email = request.Email, Password = request.Password });
            _context.SaveChanges();

            return Task.FromResult(new EmptyGrpc());
        }

        public override Task<EmptyGrpc> Update(UserGrpc request, ServerCallContext context)
        {
            return base.Update(request, context);
        }

        public override Task<EmptyGrpc> Delete(UserIdGrpc request, ServerCallContext context)
        {
            return base.Delete(request, context);
        }

        public override Task<UserGrpc> Get(UserIdGrpc request, ServerCallContext context)
        {
            return base.Get(request, context);
        }

        public override Task<UsersGrpc> GetAll(EmptyGrpc request, ServerCallContext context)
        {
            return base.GetAll(request, context);
        }

    }
}
