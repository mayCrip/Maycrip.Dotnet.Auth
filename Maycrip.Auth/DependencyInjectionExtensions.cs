using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Maycrip.Auth;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddMariaDbUserManagement(this IServiceCollection services)
    {
        services.AddTransient<IQueryRunner, MariaDbQueryRunner>();
        services.AddTransient<IUserStore<AppUser>, MariaDbUserStore>();
        services.AddTransient<IRoleStore<AppUserRole>, MariaDbUserStore>();

        return services;
    }
}