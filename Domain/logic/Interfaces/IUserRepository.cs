using Domain.Logic.Interfaces;
using Domain.Models;


namespace domain.Logic.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExists(string login);
        User GetUserByLogin(string login);
        bool CreateUser(User user);
    }
}