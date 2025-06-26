using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KartoshkaEvent.DataAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<KartoshkaEventContext>
    {
        public KartoshkaEventContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var options = new DbContextOptionsBuilder()
                .UseNpgsql(configuration["ConnectionStrings:ConnectionStringDefault"], opt =>
                {
                    opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });

            return new(options.Options);
        }
    }
}
