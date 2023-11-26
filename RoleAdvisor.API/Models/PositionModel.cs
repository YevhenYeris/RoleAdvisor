namespace RoleAdvisor.API.Models;

public class PositionModel
{
    public int ProjectId { get; set; }

    public int RoleId { get; set; }

    public int? PickedEmployeeId { get; set; }
}
