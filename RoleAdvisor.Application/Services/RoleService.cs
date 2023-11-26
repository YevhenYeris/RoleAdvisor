using Microsoft.EntityFrameworkCore;
using RoleAdvisor.Adapter;
using RoleAdvisor.Application.Interfaces;
using RoleAdvisor.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleAdvisor.Application.Services;

public class RoleService : IRoleService
{
    private readonly RoleAdvisorContext _context;

    public RoleService(RoleAdvisorContext context)
    {
        _context = context;
    }

    // Create
    public async Task<Role> AddRole(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    // Read
    public async Task<Role> GetRoleById(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task<List<Role>> GetAllRoles()
    {
        return await _context.Roles.ToListAsync();
    }

    // Update
    public async Task<Role> UpdateRole(Role role)
    {
        _context.Entry(role).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return role;
    }

    // Delete
    public async Task<bool> DeleteRole(int id)
    {
        var role = await _context.Roles.FindAsync(id);

        if (role == null)
            return false;

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return true;
    }
}
