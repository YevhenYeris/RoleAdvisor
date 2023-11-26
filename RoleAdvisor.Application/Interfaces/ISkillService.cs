using RoleAdvisor.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoleAdvisor.Application.Interfaces;

public interface ISkillService
{
    Task<Skill> AddSkill(Skill skill);
    Task<Skill> GetSkillById(int id);
    Task<List<Skill>> GetAllSkills();
    Task<Skill> UpdateSkill(Skill skill);
    Task<bool> DeleteSkill(int id);
}
