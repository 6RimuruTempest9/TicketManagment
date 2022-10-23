using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.DataAccess.Databases.EntityFrameworkCore;
using TicketManagement.Web;

namespace TicketManagement.IntegrationTests.BusinessLogic.Services
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            var dbContextDescriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<EFDbContext>));

            services.Remove(dbContextDescriptor);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddJsonFile("appsettings.testing.json")
                .Build();

            services.AddDbContext<EFDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TestingConnection"));
            });

            services.AddControllersWithViews().AddApplicationPart(Assembly.Load(new AssemblyName("TicketManagement.Web")));
        }
    }
}