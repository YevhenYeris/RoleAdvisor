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
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;

    public ProjectsController(IProjectService projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    // GET api/project
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectModel>>> GetAllProjects()
    {
        var projects = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectModel>>(await _projectService.GetAllProjects());
        return Ok(projects);
    }

    // GET api/project/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectModel>> GetProjectById(int id)
    {
        var project = await _projectService.GetProjectById(id);

        if (project == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Project, ProjectModel>(project));
    }

    // POST api/project
    [HttpPost]
    public async Task<ActionResult<ProjectModel>> AddProject([FromBody] ProjectModel project)
    {
        var newProject = await _projectService.AddProject(_mapper.Map<ProjectModel, Project>(project));
        return CreatedAtAction(nameof(GetProjectById), new { id = newProject.Id }, newProject);
    }

    // PUT api/project/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectModel>> UpdateProject(int id, [FromBody] ProjectModel project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }

        var updatedProject = await _projectService.UpdateProject(_mapper.Map<ProjectModel, Project>(project));

        if (updatedProject == null)
        {
            return NotFound();
        }

        return Ok(updatedProject);
    }

    // DELETE api/project/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteProject(int id)
    {
        var result = await _projectService.DeleteProject(id);

        if (!result)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // GET api/project/5/positions
    [HttpGet("{id}/positions")]
    public async Task<ActionResult<IEnumerable<PositionModel>>> GetProjectPositionsById(int id)
    {
        var positions = await _projectService.GetProjectPositionsById(id);

        if (positions == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<IEnumerable<Position>, IEnumerable<PositionModel>>(positions));
    }

    // POST api/project/5/positions
    [HttpPost("{id}/positions")]
    public async Task<ActionResult> AddPosition(int id, [FromBody] PositionModel position)
    {
        if (!await _projectService.AddPositionToProject(id, _mapper.Map<PositionModel, Position>(position)))
        {
            return NotFound();
        }

        return Ok();
    }
}
