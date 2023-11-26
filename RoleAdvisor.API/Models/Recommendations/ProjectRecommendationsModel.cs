using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.API.Models.Recommendations;

public class ProjectRecommendationsModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CreatorId { get; set; }

    public ICollection<PositionModel> Positions { get; set; } = new List<PositionModel>();
}
