using Domain.models;

namespace Domain.logic
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }

    public interface IUserRepository : IRepository<User>
    {
        User GetUserByLogin(string login);
        bool CreateUser(User user);
    }
}