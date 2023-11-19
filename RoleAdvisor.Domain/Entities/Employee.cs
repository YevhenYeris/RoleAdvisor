namespace RoleAdvisor.Domain.Entities;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Skill> Skills { get; set; } = null!;

    public ICollection<Role> RolesPreferred { get; set; } = null!;

    public ICollection<Project> ProjectsCreated { get; set; } = null!;

    public ICollection<Position> PositionsPickedFor { get; set; } = null!;
}
