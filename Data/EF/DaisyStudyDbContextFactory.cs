using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.EF
{
    public class DaisyStudyDbContextFactory : IDesignTimeDbContextFactory<DaisyStudyDbContext>
    {
        public DaisyStudyDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DaisyStudyDb");

            var optionsBuilder = new DbContextOptionsBuilder<DaisyStudyDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DaisyStudyDbContext(optionsBuilder.Options);
        }
    }
}

