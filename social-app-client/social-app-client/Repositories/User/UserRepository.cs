using Grpc.Net.Client;
using social_app_client.Models.User;

namespace social_app_client.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        public async Task DeleteFromQueue(Guid id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7091");
            var client = new UserStream.UserStreamClient(channel);

            await client.DeleteAsync(new UserIdGrpc { Id = id.ToString() });
        }

        public async Task<UsersGrpc> GetAllFromQueue()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7091");
            var client = new UserStream.UserStreamClient(channel);

            return await client.GetAllAsync(new EmptyGrpc());
        }

        public async Task<UserGrpc> GetFromQueue(Guid id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7091");
            var client = new UserStream.UserStreamClient(channel);

            return await client.GetAsync(new UserIdGrpc { Id = id.ToString() });
        }

        public async Task InsertIntoQueue(UserRequest request)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7091");
            var client = new UserStream.UserStreamClient(channel);

            await client.CreateAsync(new UserGrpc { Name = request.Name, Email = request.Email, Password = request.Password });
        }

        public async Task UpdateFromQueue(UserRequest request, Guid id)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7091");
            var client = new UserStream.UserStreamClient(channel);

            await client.UpdateAsync(new UpdateUser { Id = id.ToString(), Name = request.Name, Email = request.Email, Password = request.Password }); ;
        }
    }
}
