using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Application.Interfaces;

public interface IRecommendationService
{
    Task<Project> SelectBestEmployees(int projectId);
}
