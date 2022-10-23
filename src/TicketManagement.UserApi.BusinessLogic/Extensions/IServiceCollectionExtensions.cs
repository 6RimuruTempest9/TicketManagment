using Microsoft.Extensions.DependencyInjection;
using TicketManagement.UserApi.BusinessLogic.Dto;
using TicketManagement.UserApi.BusinessLogic.Services;
using TicketManagement.UserApi.BusinessLogic.Validation.Validators;

namespace TicketManagement.UserApi.BusinessLogic.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<UserManager>();
            services.AddScoped<JwtTokenManager>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<RegistrationDto>, RegistrationDtoValidator>();
            services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
        }
    }
}