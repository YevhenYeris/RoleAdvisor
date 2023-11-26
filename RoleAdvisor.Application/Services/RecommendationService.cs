using RoleAdvisor.Adapter;
using RoleAdvisor.Application.Interfaces;
using RoleAdvisor.Application.Algorithm;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Application.Services;

public class RecommendationService : IRecommendationService
{
    private readonly IProjectService _projectService;
    private readonly IEmployeeService _employeeService;
    private readonly IReadService<Employee> _readService;
    private readonly TOPSIS _topsis;

    public RecommendationService(IProjectService projectService, IEmployeeService employeeService, IReadService<Employee> readService)
    {
        _projectService = projectService;
        _employeeService = employeeService;
        _topsis = new TOPSIS();
        _readService = readService;
    }

    public async Task<Project> SelectBestEmployees(int projectId)
    {
        var project = await _projectService.GetProjectById(projectId);
        var employees = await _readService.GetAllEntities();

        var recommendation = _topsis.SelectBestEmployees(employees, project.Positions.ToList());

        for (var i = 0; i < project.Positions.Count; ++i)
        {
            var employee = recommendation.ElementAtOrDefault(i);

            if (employee is not null)
            {
                project.Positions.ElementAt(0).AdvisedEmployees.Add(employee);
            }
        }

        return project;
    }
}
