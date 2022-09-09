using social_app_client.Models.User;

namespace social_app_client.Repositories.User
{
    public interface IUserRepository
    {
        Task InsertIntoQueue(UserRequest request);
        Task UpdateFromQueue(UserRequest request, Guid id);
        Task DeleteFromQueue(Guid id);
        Task<UserGrpc> GetFromQueue(Guid id);
        Task<UsersGrpc> GetAllFromQueue();
    }
}
