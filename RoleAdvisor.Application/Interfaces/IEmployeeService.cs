using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Application.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Role>> GetEmployeeRolesById(int id);
    Task<bool> AddEmployeeRole(int id, int roleId);

    Task<IEnumerable<Skill>> GetEmployeeSkillsById(int id);
    Task<bool> AddEmployeeSkill(int id, int skillId);
}
