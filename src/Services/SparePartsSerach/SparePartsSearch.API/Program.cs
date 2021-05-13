using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Reflection;

namespace SparePartsSearch.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = CreateSerilogLogger();
            var projectName = Assembly.GetCallingAssembly().GetName();
            Log.Logger.Information($"------------------{projectName} STARTED--------------");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog((context, services, configuration) => configuration
                    .Enrich.FromLogContext()
                    .ReadFrom.Services(services)
                    .WriteTo.File("/src/Logger/logSparePartsSearch.txt")
                    .WriteTo.Console())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static Serilog.ILogger CreateSerilogLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File("/src/Logger/logSparePartsSearch.txt")
                .CreateBootstrapLogger();
        }
    }
}
