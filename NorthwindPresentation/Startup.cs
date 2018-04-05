using System;
using System.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Ef;
using NorthwindApplication.Customer;
using NorthwindApplication.Customer.Actions;

namespace NorthwindPresentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            
            var connectionString = Configuration.GetConnectionString("Northwind");
                services.AddEntityFrameworkNpgsql().AddDbContext<NorthwindContext>(options => options
                .UseNpgsql(connectionString)
            );
            
            // autofac configuration
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule<CustomerModule>();
            this.AutofacContainer = builder.Build();
          
            return new AutofacServiceProvider(this.AutofacContainer);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
            
            StoreContainer.CustomerStore = new CustomerManager(this.AutofacContainer).CustomerStore;
            StoreContainer.CustomerStore.Dispatch(new LoadCustomerListAction());
        }
    }
}