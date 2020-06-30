using Gelf.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace LoggingExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) => Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .ConfigureLogging((context, builder) => builder.AddGelf(options =>
                        {
                            // Optional customisation applied on top of settings in Logging:GELF configuration section.
                            options.LogSource = context.HostingEnvironment.ApplicationName;
                            options.AdditionalFields["machine_name"] = Environment.MachineName;
                            options.AdditionalFields["app_version"] = Assembly.GetEntryAssembly()
                                ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
                        }));
                })  ;

    }
}

//    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
//        WebHost.CreateDefaultBuilder(args)
//        .UseStartup<Startup>()
//        .ConfigureLogging(loggingConfiguration => loggingConfiguration.ClearProviders())
//        .UseSerilog((hostingContext, loggerConfiguration) =>
//            loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
//}
