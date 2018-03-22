using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Ef;
using NorthwindTraders.Persistance;

namespace NorthwindMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = BuildConfiguration();

            var serviceProvider = BuildServiceProvider(config);

            using (var ctx = serviceProvider.GetService<NorthwindContext>())
            {
                ctx.Database.Migrate();
                
                NorthwindInitializer.Initialize(ctx);

                var countCompanies = ctx.Customers.Count();
                Console.Out.WriteLine($"DB initialised with {countCompanies} customers.");
            }            
        }

        private static ServiceProvider BuildServiceProvider(IConfigurationRoot config)
        {
            var connectionString = config.GetConnectionString("Northwind");
            
            var serviceCollection = new ServiceCollection()
                .AddEntityFrameworkNpgsql().AddDbContext<NorthwindContext>(options => options
                    .UseNpgsql(connectionString)
                );
                
                return serviceCollection.BuildServiceProvider();
        }


        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.localdev.json", optional: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}