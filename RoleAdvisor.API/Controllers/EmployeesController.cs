using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoleAdvisor.API.Models;
using RoleAdvisor.Application.Interfaces;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IReadService<Employee> _readService;
    private readonly IWriteService<Employee> _writeService;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeService employeeService, IReadService<Employee> readService, IWriteService<Employee> writeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
        _readService = readService;
        _writeService = writeService;
    }

    // GET api/employee
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetAllEmployees()
    {
        var employees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeModel>>(await _readService.GetAllEntities());
        return Ok(employees);
    }

    // GET api/employee/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeModel>> GetEmployeeById(int id)
    {
        var employee = await _readService.GetEntityById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Employee, EmployeeModel>(employee));
    }

    // POST api/employee
    [HttpPost]
    public async Task<ActionResult<EmployeeModel>> AddEmployee([FromBody] EmployeeModel employee)
    {
        var newEmployee = await _writeService.AddEntity(_mapper.Map<EmployeeModel, Employee>(employee));
        return CreatedAtAction(nameof(GetEmployeeById), new { id = newEmployee.Id }, newEmployee);
    }

    // PUT api/employee/5
    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeModel>> UpdateEmployee(int id, [FromBody] EmployeeModel employee)
    {
        if (id != employee.Id)
        {
            return BadRequest();
        }

        var updatedEmployee = await _writeService.UpdateEntity(_mapper.Map<EmployeeModel, Employee>(employee));

        if (updatedEmployee == null)
        {
            return NotFound();
        }

        return Ok(updatedEmployee);
    }

    // DELETE api/employee/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteEmployee(int id)
    {
        var result = await _writeService.DeleteEntity(id);

        if (!result)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // GET api/employee/5/roles
    [HttpGet("{id}/roles")]
    public async Task<ActionResult<IEnumerable<RoleModel>>> GetEmployeeRolesById(int id)
    {
        var roles = await _employeeService.GetEmployeeRolesById(id);

        if (roles == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(roles));
    }

    // POST api/employee/5/roles
    [HttpPost("{id}/roles")]
    public async Task<ActionResult> AddRole(int id, [FromQuery] int roleId)
    {

        if(!await _employeeService.AddEmployeeRole(id, roleId))
        {
            return NotFound();
        }

        return Ok();
    }

    // GET api/employee/5/skills
    [HttpGet("{id}/skills")]
    public async Task<ActionResult<IEnumerable<SkillModel>>> GetEmployeeSkillsById(int id)
    {
        var skills = await _employeeService.GetEmployeeSkillsById(id);

        if (skills == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<Skill>, IEnumerable<SkillModel>>(skills));
    }

    // POST api/employee/5/skills
    [HttpPost("{id}/skills")]
    public async Task<ActionResult> AddSkill(int id, [FromQuery] int skillId)
    {
        if (!await _employeeService.AddEmployeeSkill(id, skillId))
        {
            return NotFound();
        }

        return Ok();
    }
}
