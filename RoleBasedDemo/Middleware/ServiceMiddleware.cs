using RoleBasedDemo.Helpers;
using RoleBasedDemo.Services;
using Microsoft.Extensions.Configuration;


namespace RoleBasedDemo.Middleware
{
    public static class ServiceMiddleware
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IRegisterService, RegisterService>();

            // InactivityRedirec
            // services.AddMiddleware<CookieExpirationMiddleware>();
            //services.AddScoped<InactivityRedirec, InactivityRedirec>();

            services.AddSingleton(new ConnectionStrings { SQL_Connection = config.GetConnectionString("SQL_Connection") });
            return services;
        }
    }
}