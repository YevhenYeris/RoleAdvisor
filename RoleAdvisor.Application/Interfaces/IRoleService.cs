using RoleAdvisor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoleAdvisor.Application.Interfaces;

public interface IRoleService
{
    Task<Role> AddRole(Role role);
    Task<Role> GetRoleById(int id);
    Task<List<Role>> GetAllRoles();
    Task<Role> UpdateRole(Role role);
    Task<bool> DeleteRole(int id);
}
