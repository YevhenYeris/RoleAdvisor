using Microsoft.EntityFrameworkCore;
using RoleAdvisor.Adapter;
using RoleAdvisor.Application.Interfaces;
using RoleAdvisor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoleAdvisor.Application.Services;

public class SkillService : ISkillService
{
    private readonly RoleAdvisorContext _context;

    public SkillService(RoleAdvisorContext context)
    {
        _context = context;
    }

    // Create
    public async Task<Skill> AddSkill(Skill skill)
    {
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();
        return skill;
    }

    // Read
    public async Task<Skill> GetSkillById(int id)
    {
        return await _context.Skills.FindAsync(id);
    }

    public async Task<List<Skill>> GetAllSkills()
    {
        return await _context.Skills.ToListAsync();
    }

    // Update
    public async Task<Skill> UpdateSkill(Skill skill)
    {
        _context.Entry(skill).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return skill;
    }

    // Delete
    public async Task<bool> DeleteSkill(int id)
    {
        var skill = await _context.Skills.FindAsync(id);

        if (skill == null)
            return false;

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();
        return true;
    }
}
