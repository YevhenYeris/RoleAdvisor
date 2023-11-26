namespace RoleAdvisor.Domain.Entities;

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CreatorId { get; set; }

    public ICollection<Position> Positions { get; set; } = new List<Position>();
}
