using Microsoft.EntityFrameworkCore;
using Database;
using Database.Repository;

using Microsoft.Extensions.Configuration;

namespace Tests
{
    public class DatabaseTest
    {
        private readonly DbContextOptionsBuilder<ApplicationContext> _contextOptionsBuilder;

        public DatabaseTest()
        {
            
            var configuration = 
                new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..")))
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=backend;" +
                                     "Username=postgres;Password=12345678");
            _contextOptionsBuilder = optionsBuilder;
        }

        [Fact]
        public void UserCreation()
        {
            var context = new ApplicationContext(_contextOptionsBuilder.Options);
            var UserRepository = new UserRepository(context);
            UserRepository.Create(new Domain.Models.User(0, "123", "fullname", "name", "12345678"));
            context.SaveChanges();
            Assert.True(context.Users.Any(u => u.UserName == "name"));
        }

        [Fact]
        public void DatabaseAdd() 
        {
            using var context = new ApplicationContext(_contextOptionsBuilder.Options);
            context.Users.Add(new Database.Models.User()
            {
                UserName = "admin"
            });
            context.SaveChanges();
            Assert.True(context.Users.Any(u => u.UserName == "admin"));
        }

        [Fact]
        public void GetFirstElementFromDB()
        {
            using var context = new ApplicationContext(_contextOptionsBuilder.Options);
            var user = context.Users.FirstOrDefault(u => u.UserName == "name");
            context.Users.Remove(user!);
            context.SaveChanges();
            Assert.True(!context.Users.Any(u => u.UserName == "name"));
        }

        [Fact]
        public void GetByLogin()
        {
            using var context = new ApplicationContext(_contextOptionsBuilder.Options);
            var userRepository = new UserRepository(context);
            var userService = new UserInteractor(userRepository);
            var res = userService.GetUserByLogin("admin");
            Assert.NotNull(res.Value);
        }
    }
}
