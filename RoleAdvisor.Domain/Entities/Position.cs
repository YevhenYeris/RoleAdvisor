namespace RoleAdvisor.Domain.Entities;

public class Position
{
    public int ProjectId { get; set; }

    public int RoleId { get; set; }

    public int? PickedEmployeeId { get; set; }

    public ICollection<Skill> SkillsRequired { get; set; } = new List<Skill>();

    public ICollection<Employee> AdvisedEmployees { get; set; } = new List<Employee>();
}
