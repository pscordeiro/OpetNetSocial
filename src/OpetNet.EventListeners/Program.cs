using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpetNet.Infra.CrossCutting.IoC;
using MediatR;
using System;

namespace OpetNet.EventListeners
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder();

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            });
            builder.ConfigureServices((hostingContext, services) =>
            {
                services.RegisterServices(hostingContext.Configuration);
                services.AddOptions();       
                services.AddMediatR(typeof(Program));
                services.AddSingleton<IHostedService, EventListenerService>();
            });

            builder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddLog4Net();
            });

            await builder.RunConsoleAsync();
        }
    }
}
