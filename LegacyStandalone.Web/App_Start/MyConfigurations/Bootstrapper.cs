using System.Web.Http;
using LegacyStandalone.Web.MyConfigurations.Log;
using LegacyStandalone.Web.MyConfigurations.Mapping;

namespace LegacyStandalone.Web.MyConfigurations
{
    public class Bootstrapper
    {
        public static void Run()
        {
            // Serilog
            SerilogConfiguration.CreateLogger();
            // Configure Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
            // Configure AutoMapper
            AutoMapperConfiguration.Configure();
        }
    }
}