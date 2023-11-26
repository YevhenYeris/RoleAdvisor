using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Application.Interfaces;

public interface IEmployeeService
{
    Task<Employee> AddEmployee(Employee employee);
    Task<Employee> GetEmployeeById(int id);
    Task<List<Employee>> GetAllEmployees();
    Task<Employee> UpdateEmployee(Employee employee);
    Task<bool> DeleteEmployee(int id);

    Task<IEnumerable<Role>> GetEmployeeRolesById(int id);
    Task<bool> AddEmployeeRole(int id, int roleId);

    Task<IEnumerable<Skill>> GetEmployeeSkillsById(int id);
    Task<bool> AddEmployeeSkill(int id, int skillId);
}
