using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace YarYab.Data
{
    public class YarYabContextFactory : IDesignTimeDbContextFactory<YarYabContext>
    {
        public YarYabContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YarYabContext>();

            // Configure the context options (e.g., specify the connection string)
            var configuration = new ConfigurationBuilder()
                //.SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json")
                .Build();

            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql("User ID=postgres;Password=a@12345;Host=localhost;Port=5432;Database=YarYab;Connection Lifetime=0;");

            return new YarYabContext(optionsBuilder.Options);
        }
    }
}