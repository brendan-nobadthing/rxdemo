using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Northwind.Ef
{
    public class DesignTimeContextFactory: IDesignTimeDbContextFactory<NorthwindContext>
    {
        public NorthwindContext CreateDbContext(string[] args)
        {
            var config = BuildConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("Northwind"));
            
            return new NorthwindContext(optionsBuilder.Options);
        }
        
        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.localdev.json", optional: true);
            return builder.Build();
        }
    }
}