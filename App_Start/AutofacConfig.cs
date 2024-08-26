using Autofac;
using Autofac.Integration.Mvc;
using G_APIs;
using G_APIs.BussinesLogic;
using G_APIs.BussinesLogic.Interface;
using System.Web.Mvc;

public class AutofacConfig
{
    public static void RegisterDependencies()
    {
        ContainerBuilder builder = new ContainerBuilder();

        // Register your MVC controllers.
        builder.RegisterControllers(typeof(MvcApplication).Assembly);

        // Register your types
        builder.RegisterType<Account>().As<IAccount>().InstancePerRequest();
        builder.RegisterType<SessionManager>().As<ISession>().InstancePerRequest();
        builder.RegisterType<Fund>().As<IFund>().InstancePerRequest();
        builder.RegisterType<Reports>().As<IReports>().InstancePerRequest();
        builder.RegisterType<Dashboard>().As<IDashboard>().InstancePerRequest();
        builder.RegisterType<Store>().As<IStore>().InstancePerRequest();
        builder.RegisterType<Settings>().As<ISettings>().InstancePerRequest();

        // Set the dependency resolver to be Autofac.
        IContainer container = builder.Build();
        DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
    }
}