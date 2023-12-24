using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using OwinWebApi;

[assembly: OwinStartup(typeof(Startup))]
namespace OwinWebApi
{
    public class Startup
        : IDisposable
    {
        private HttpConfiguration _httpConfiguration;

        public void Configuration(IAppBuilder app)
        {
            _httpConfiguration?.Dispose();
            _httpConfiguration = new HttpConfiguration();

            ConfigureFormatter();
            ConfigureRouting();
            

            app.UseWebApi(_httpConfiguration);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpConfiguration?.Dispose();
            }
        }

        private void ConfigureFormatter()
        {
            var defaultSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
#if DEBUG
                Formatting = Formatting.Indented,
#else
                Formatting = Formatting.None,
#endif
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            JsonConvert.DefaultSettings = () => defaultSettings;

            _httpConfiguration.Formatters.Clear();
            _httpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());
            _httpConfiguration.Formatters.JsonFormatter.SerializerSettings = defaultSettings;
        }

        private void ConfigureRouting()
        {
            // Web API routes
            _httpConfiguration.MapHttpAttributeRoutes();

            _httpConfiguration.Routes.MapHttpRoute(
                name: "Owin Web API",
                routeTemplate: "{controller}/{id}",
                defaults: new { controller = "Home", id = RouteParameter.Optional });
        }
    }
}