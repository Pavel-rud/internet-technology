using Database.Converters;
using Domain.Logic.Interfaces;
using Domain.Models;

namespace Database.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationContext context;

        public UserRepository(ApplicationContext context) {
            this.context = context;
        }

        public User Create(User item) {
            return context.Add(item.ToModel()).Entity.ToDomain();
        }

        public User? Delete(int id) {
            var item = GetItem(id);
            if (item == default)
                return null;
            return context.Remove(item.ToModel()).Entity.ToDomain();
        }

        public IEnumerable<User> GetAll() {
            return context.Users.Select(item => item.ToDomain());
        }

        public User? GetItem(int id) {
            return context.Users.FirstOrDefault(item => item.Id == id)?.ToDomain();
        }

        public User? GetUserByLogin(string login) {
            return context.Users
                .FirstOrDefault(item => item.UserName == login)?
                .ToDomain();
        }

        public void Save() {
            context.SaveChangesAsync();
        }

        public User Update(User item) {
            return context.Users.Update(item.ToModel()).Entity.ToDomain();
        }
    }
}
