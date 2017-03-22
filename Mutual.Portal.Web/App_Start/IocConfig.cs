using Autofac;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Mutual.Portal.Core.Persistence;
using Mutual.Portal.Service.BusinessLogic.NurseManagement;
using Mutual.Portal.Service.BusinessLogic.UserManagement;

namespace Mutual.Portal.Web.App_Start
{
    public class IocConfig
    {
        public static void ConfigDIContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<OperationContext>().As<IOperationDbContext>().SingleInstance();

            builder.RegisterType<NurseManager>().As<INurseManager>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserManager>().SingleInstance();


            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}