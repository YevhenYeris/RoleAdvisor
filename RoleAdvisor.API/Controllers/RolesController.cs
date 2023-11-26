using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoleAdvisor.API.Models;
using RoleAdvisor.Application.Interfaces;
using RoleAdvisor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoleAdvisor.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RolesController(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    // GET api/roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleModel>>> GetAllRoles()
    {
        var roles = _mapper.Map<IEnumerable<Role>, IEnumerable<RoleModel>>(await _roleService.GetAllRoles());
        return Ok(roles);
    }

    // GET api/roles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RoleModel>> GetRoleById(int id)
    {
        var role = await _roleService.GetRoleById(id);

        if (role == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Role, RoleModel>(role));
    }

    // POST api/roles
    [HttpPost]
    public async Task<ActionResult<RoleModel>> AddRole([FromBody] RoleModel role)
    {
        var newRole = await _roleService.AddRole(_mapper.Map<RoleModel, Role>(role));
        return CreatedAtAction(nameof(GetRoleById), new { id = newRole.Id }, newRole);
    }

    // PUT api/roles/5
    [HttpPut("{id}")]
    public async Task<ActionResult<RoleModel>> UpdateRole(int id, [FromBody] RoleModel role)
    {
        if (id != role.Id)
        {
            return BadRequest();
        }

        var updatedRole = await _roleService.UpdateRole(_mapper.Map<RoleModel, Role>(role));

        if (updatedRole == null)
        {
            return NotFound();
        }

        return Ok(updatedRole);
    }

    // DELETE api/roles/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteRole(int id)
    {
        var result = await _roleService.DeleteRole(id);

        if (!result)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
