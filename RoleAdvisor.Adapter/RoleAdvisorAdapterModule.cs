using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;

namespace RoleAdvisor.Adapter;

public static class RoleAdvisorAdapterModule
{
    public static IServiceCollection AddRoleAdvisorAdapterModule(this IServiceCollection services,
        string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));

        services.AddDbContext<RoleAdvisorContext>(opt => opt.UseSqlServer(connectionString));
        return services;
    }
}
