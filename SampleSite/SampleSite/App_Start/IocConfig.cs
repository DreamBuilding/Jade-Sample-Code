using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using SampleSite.Controllers;
using SampleSite.Converters;

namespace SampleSite
{
    public static class IocConfig
    {
        public static IContainer UseIoc(this IAppBuilder app)
        {
            // Create the container builder.
            var builder = new ContainerBuilder();
            {
                builder.RegisterType<DecimalToTextConverter>().As<INumberToTextConverter<decimal>>();

                // Register the Web API controllers.
                var assembly = Assembly.GetExecutingAssembly();
                builder.RegisterApiControllers(assembly);

                // Build the container.
                var container = builder.Build();

                return container;
            }
        }

    }
}