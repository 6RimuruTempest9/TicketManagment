using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using Serilog;
using TicketManagement.Web;

namespace TicketManagement.IntegrationTests.BusinessLogic.Services
{
    public class ServiceTestBase
    {
        private protected IServiceProvider Provider { get; private set; }

        [OneTimeSetUp]
        public async Task CreateService()
        {
            var hostBuilder = Host
                .CreateDefaultBuilder()
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.UseStartup<TestStartup>();
                    webBuilder.UseSetting(WebHostDefaults.ApplicationKey, typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                });

            var host = await hostBuilder.StartAsync();

            Provider = host.Services.CreateScope().ServiceProvider;
        }
    }
}