using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using RestEase;
using Serilog;
using TicketManagement.BusinessLogic.Extensions;
using TicketManagement.CommonElements.Extensions;
using TicketManagement.DataAccess.Databases.EntityFrameworkCore;
using TicketManagement.Web.Extensions;
using TicketManagement.Web.Filters;
using TicketManagement.Web.HttpClients;
using TicketManagement.Web.Middlewares;

namespace TicketManagement.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddFeatureManagement()
                .UseDisabledFeaturesHandler(new RedirectToReactFilter(Configuration.GetValue<string>("ReactURL")));

            services.AddExceptionHelper();

            services.AddDbContext<EFDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpClientOptions(Configuration);

            services.AddHttpClients();

            services.AddServices();

            services.AddControllersWithViews().AddViewLocalization();

            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.AddDistributedMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("ru-RU"),
                new CultureInfo("be-BY"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            });

            app.UseSession();
            app.UseJwtAttachingToHttpClients();
            app.UseRolesAttaching();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseMiddleware<LocalizeMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Auth",
                    pattern: "Authorization/{action}",
                    defaults: new { controller = "Authorization" });

                endpoints.MapControllerRoute(
                    name: "Events",
                    pattern: "Events/{action=GetAll}/{id?}",
                    defaults: new { controller = "Event" });

                endpoints.MapControllerRoute(
                    name: "EventAreas",
                    pattern: "EventAreas/{action}/{id?}",
                    defaults: new { controller = "EventArea" });

                endpoints.MapControllerRoute(
                    name: "EventSeats",
                    pattern: "EventSeats/{action}/{id?}",
                    defaults: new { controller = "EventSeat" });

                endpoints.MapControllerRoute(
                    name: "Purchase",
                    pattern: "Purchase/{action}",
                    defaults: new { controller = "Purchase" });

                endpoints.MapControllerRoute(
                    name: "User",
                    pattern: "User/{action}",
                    defaults: new { controller = "User" });

                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=Event}/{action=GetAll}/{id?}");
            });
        }
    }
}