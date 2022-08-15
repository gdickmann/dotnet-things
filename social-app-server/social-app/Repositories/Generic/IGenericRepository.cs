using social_app.Api.Models;

namespace social_app.Repositories.Generic
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(Guid id);
        List<T> GetAll();
    }
}
