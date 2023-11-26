using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.API.Models.Recommendations;

public class PositionRecommendationsModel
{
    public int ProjectId { get; set; }

    public int RoleId { get; set; }

    public int? PickedEmployeeId { get; set; }

    public ICollection<SkillModel> SkillsRequired { get; set; } = new List<SkillModel>();

    public ICollection<EmployeeModel> AdvisedEmployees { get; set; } = new List<EmployeeModel>();
}
