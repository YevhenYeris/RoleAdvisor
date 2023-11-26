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
public class SkillsController : ControllerBase
{
    private readonly ISkillService _skillService;
    private readonly IMapper _mapper;

    public SkillsController(ISkillService skillService, IMapper mapper)
    {
        _skillService = skillService;
        _mapper = mapper;
    }

    // GET api/skills
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SkillModel>>> GetAllSkills()
    {
        var skills = _mapper.Map<IEnumerable<Skill>, IEnumerable<SkillModel>>(await _skillService.GetAllSkills());
        return Ok(skills);
    }

    // GET api/skills/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SkillModel>> GetSkillById(int id)
    {
        var skill = await _skillService.GetSkillById(id);

        if (skill == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Skill, SkillModel>(skill));
    }

    // POST api/skills
    [HttpPost]
    public async Task<ActionResult<SkillModel>> AddSkill([FromBody] SkillModel skill)
    {
        var newSkill = await _skillService.AddSkill(_mapper.Map<SkillModel, Skill>(skill));
        return CreatedAtAction(nameof(GetSkillById), new { id = newSkill.Id }, newSkill);
    }

    // PUT api/skills/5
    [HttpPut("{id}")]
    public async Task<ActionResult<SkillModel>> UpdateSkill(int id, [FromBody] SkillModel skill)
    {
        if (id != skill.Id)
        {
            return BadRequest();
        }

        var updatedSkill = await _skillService.UpdateSkill(_mapper.Map<SkillModel, Skill>(skill));

        if (updatedSkill == null)
        {
            return NotFound();
        }

        return Ok(updatedSkill);
    }

    // DELETE api/skills/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteSkill(int id)
    {
        var result = await _skillService.DeleteSkill(id);

        if (!result)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
