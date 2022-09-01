using Grpc.Net.Client;
using social_app_client.Models.User;

namespace social_app_client.Repository.User
{
    public class UserRepository : IUserRepository
    {
        public async Task DeleteFromQueue(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task GetAllFromQueue()
        {
            throw new NotImplementedException();
        }

        public async Task GetFromQueue(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task InsertIntoQueue(UserRequest request)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7091");
            var client = new UserStream.UserStreamClient(channel);

            await client.CreateAsync(new UserGrpc { Name = request.Name, Email = request.Email, Password = request.Password });
        }

        public async Task UpdateFromQueue(UserRequest request, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
