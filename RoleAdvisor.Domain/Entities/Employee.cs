namespace RoleAdvisor.Domain.Entities;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Skill> Skills { get; set; } = null!;

    public ICollection<Role> RolesPreferred { get; set; } = new List<Role>();

    public ICollection<Project> ProjectsCreated { get; set; } = new List<Project>();

    public ICollection<Position> PositionsPickedFor { get; set; } = new List<Position>();
}
