using social_app.Models;
using social_app.Models.Request;

namespace social_app.Repositories
{
    public interface IUserRepository
    {
        void Create(UserRequest user);
        void Update(User user);
        void Delete(User user);
        Task<User> GetById(Guid id);
        Task<List<User>> GetAll();
    }
}
