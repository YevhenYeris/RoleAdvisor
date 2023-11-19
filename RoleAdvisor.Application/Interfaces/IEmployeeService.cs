using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Application.Interfaces;

public interface IEmployeeService
{
    Task<Employee> AddEmployee(Employee employee);
    Task<Employee> GetEmployeeById(int id);
    Task<List<Employee>> GetAllEmployees();
    Task<Employee> UpdateEmployee(Employee employee);
    Task<bool> DeleteEmployee(int id);
}
