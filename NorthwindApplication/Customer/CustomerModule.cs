using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace NorthwindApplication.Customer
{
    public class CustomerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register all ActionEffects
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Effect"))
                .AsSelf()
                .InstancePerLifetimeScope();
       
            base.Load(builder);
        }
    }
}