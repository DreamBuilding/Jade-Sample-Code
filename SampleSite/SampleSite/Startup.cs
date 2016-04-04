using System.Web.Http;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SampleSite.Startup))]

namespace SampleSite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        { 
            var config = new HttpConfiguration();

            var container = app.UseIoc();

            app.UseAutofacMiddleware(container);

            app.UseWebApiConfig(config);

            // Configure Web API with the dependency resolver.
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
