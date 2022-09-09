namespace social_app.Repositories.User
{
    public interface IUserRepository
    {
        void Create(Models.User user);

        void Update(Models.User user);

        void Delete(Models.User user);

        Models.User? Get(Guid id);

        UsersGrpc GetAll();
    }
}
