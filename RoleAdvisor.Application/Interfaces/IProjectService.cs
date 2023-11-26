using RoleAdvisor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoleAdvisor.Application.Interfaces;

public interface IProjectService
{
    Task<Project> AddProject(Project project);
    Task<Project> GetProjectById(int id);
    Task<List<Project>> GetAllProjects();
    Task<Project> UpdateProject(Project project);
    Task<bool> DeleteProject(int id);

    Task<IEnumerable<Position>> GetProjectPositionsById(int id);
    Task<bool> AddPositionToProject(int projectId, Position position);
}
