using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Owin;

namespace SampleSite
{
    public static class WebApiConfig
    {
        public static void UseWebApiConfig(this IAppBuilder app, HttpConfiguration config)
        {
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);
        }
    }
}
