namespace ExchangeRateInfoAPI.App_Start
{
    using Autofac;
    using Autofac.Integration.WebApi;
    using ExchangeRateBL.BL;
    using ExchangeRateInfoAPI.Controllers;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;

    /// <summary>
    /// Initialization of autofac container to implement DI
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Method to register all component with autofac container
        /// </summary>
        public static void AutofacConfiguration()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(typeof(ExchangeRateController).Assembly);
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            builder.RegisterType<ExchangeRateManager>().As<IExchangeRateManager>().SingleInstance();
            builder.RegisterType<HttpClient>().As<HttpClient>().SingleInstance();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}