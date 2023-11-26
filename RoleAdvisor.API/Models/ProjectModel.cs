namespace RoleAdvisor.API.Models;

public class ProjectModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CreatorId { get; set; }
}
