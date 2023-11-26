using Microsoft.EntityFrameworkCore;
using RoleAdvisor.Adapter;
using RoleAdvisor.Application.Interfaces;
using RoleAdvisor.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleAdvisor.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly RoleAdvisorContext _context;

    public EmployeeService(RoleAdvisorContext context)
    {
        _context = context;
    }

    // Create
    public async Task<Employee> AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    // Read
    public async Task<Employee> GetEmployeeById(int id)
    {
        return (await _context.Employees
            .Include(e => e.Skills)
            .Include(e => e.RolesPreferred)
            .Include(e => e.ProjectsCreated)
            .Include(e => e.PositionsPickedFor)
            .FirstOrDefaultAsync(e => e.Id == id))!;
    }

    public async Task<List<Employee>> GetAllEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    // Update
    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return employee;
    }

    // Delete
    public async Task<bool> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);

        if (employee == null)
            return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Role>> GetEmployeeRolesById(int id)
    {
        var employee = await GetEmployeeById(id);

        if (employee == null)
            return null!;

        return employee.RolesPreferred;
    }

    public async Task<bool> AddEmployeeRole(int id, int roleId)
    {
        var employee = await GetEmployeeById(id);

        if (employee == null)
            return false;

        var role = await _context.Roles.FindAsync(roleId);

        if (role == null)
            return false;

        employee.RolesPreferred ??= new List<Role>();
        employee.RolesPreferred.Add(role);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Skill>> GetEmployeeSkillsById(int id)
    {
        var employee = await GetEmployeeById(id);

        if (employee == null)
            return null!;

        return employee.Skills;
    }

    public async Task<bool> AddEmployeeSkill(int id, int skillId)
    {
        var employee = await GetEmployeeById(id);

        if (employee == null)
            return false;

        var skill = await _context.Skills.FindAsync(skillId);

        if (skill == null)
            return false;

        employee.Skills ??= new List<Skill>();
        employee.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return true;
    }
}
