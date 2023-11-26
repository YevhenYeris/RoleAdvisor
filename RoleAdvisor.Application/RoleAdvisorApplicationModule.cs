using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;
using RoleAdvisor.Application.Interfaces;
using RoleAdvisor.Application.Services;
using System.Security.Cryptography.X509Certificates;

namespace RoleAdvisor.Adapter;

public static class RoleAdvisorApplicationModule
{
    public static IServiceCollection AddRoleAdvisorApplicationModule(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ISkillService, SkillService>();
        services.AddScoped<IRecommendationService, RecommendationService>();

        return services;
    }
}
