using Microsoft.EntityFrameworkCore;
using RoleAdvisor.Adapter;
using RoleAdvisor.Application.Interfaces;
using RoleAdvisor.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleAdvisor.Application.Services;

internal class ProjectService : IProjectService
{
    private readonly RoleAdvisorContext _context;

    public ProjectService(RoleAdvisorContext context)
    {
        _context = context;
    }

    // Create
    public async Task<Project> AddProject(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    // Read
    public async Task<Project> GetProjectById(int id)
    {
        return await _context.Projects
            .Include(p => p.Positions)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Project>> GetAllProjects()
    {
        return await _context.Projects.ToListAsync();
    }

    // Update
    public async Task<Project> UpdateProject(Project project)
    {
        _context.Entry(project).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return project;
    }

    // Delete
    public async Task<bool> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
            return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Position>> GetProjectPositionsById(int id)
    {
        var project = await GetProjectById(id);

        if (project == null)
            return null!;

        return project.Positions;
    }

    public async Task<bool> AddPositionToProject(int projectId, Position position)
    {
        var project = await GetProjectById(projectId);

        if (project == null)
            return false;

        project.Positions ??= new List<Position>();
        project.Positions.Add(position);
        await _context.SaveChangesAsync();
        return true;
    }
}
