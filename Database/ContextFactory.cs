using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Database
{
    public class ContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables(".env");
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            _ = optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=backend;" +
                                         "Username=postgres;Password=12345678");
            
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
