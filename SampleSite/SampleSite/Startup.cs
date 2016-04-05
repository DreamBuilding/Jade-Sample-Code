using System.Web.Http;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
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

            const string rootFolder = "./views";
            var fileSystem = new PhysicalFileSystem(rootFolder);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fileSystem
            };

            app.UseFileServer(options);
        }
    }
}
