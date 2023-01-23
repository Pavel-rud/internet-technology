using Domain.Models;

namespace Domain.Logic.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetItem(int id);
        T Create(T item);
        T? Update(T item);
        T? Delete(int id);
        void Save();
    }
}