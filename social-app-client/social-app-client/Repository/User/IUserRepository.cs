using social_app_client.Models.User;

namespace social_app_client.Repository.User
{
    public interface IUserRepository
    {

        Task InsertIntoQueue(UserRequest request);
        Task UpdateFromQueue(UserRequest request, Guid id);
        Task DeleteFromQueue(Guid id);
        Task GetFromQueue(Guid id);
        Task GetAllFromQueue();

    }
}
