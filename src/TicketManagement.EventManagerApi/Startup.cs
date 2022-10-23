using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Serilog;
using TicketManagement.CommonElements.Extensions;
using TicketManagement.DataAccess.Databases.EntityFrameworkCore;
using TicketManagement.DataAccess.Extensions;
using TicketManagement.EventManagerApi.Extensions;
using TicketManagement.EventManagerApi.HttpClients;

namespace TicketManagement.EventManagerApi
{
    public class Startup
    {
        private static readonly Lazy<string> XmlCommentsFilePathLazy = new Lazy<string>(XmlDocumentationPath(typeof(Startup)));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCors", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddExceptionHelper();

            services.AddDbContext<EFDbContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddRepositories();

            services.AddValidators();

            services.AddServices();

            services.AddControllers(options =>
            {
                options.RequireHttpsPermanent = true;
                options.RespectBrowserAcceptHeader = true;
            });

            services.AddScoped<JwtValidationHttpClient>();

            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                setup.IncludeXmlComments(XmlCommentsFilePathLazy.Value);

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "Jwt Token is required to access the endpoints",
                    In = ParameterLocation.Header,
                    Name = "JWT Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    },
                };

                setup.AddSecurityDefinition("Bearer", jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() },
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseSerilogRequestLogging();

            app.UseCors("AllowCors");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static string XmlDocumentationPath(Type currentClass)
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var fileName = currentClass.GetTypeInfo().Assembly.GetName().Name + ".xml";

            return Path.Combine(basePath, fileName);
        }
    }
}