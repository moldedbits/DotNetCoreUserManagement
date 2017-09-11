using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using UserAppService.Context;
using UserAppService.Service;

namespace UserManagement.Web.IoC
{
    public class TypeRegistrar
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            #region App Service Injections

            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerRequest();
            #endregion

            #region DBContext

            builder.RegisterType<ApplicationDbContext>().As<ApplicationDbContext>().InstancePerRequest(); 

            #endregion

            var container = builder.Build();
            return container;
        }
    }
}