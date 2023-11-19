namespace RoleAdvisor.Domain.Entities;

public class Position
{
    public int ProjectId { get; set; }

    public int RoleId { get; set; }

    public int PickedEmployeeId { get; set; }

    public ICollection<Employee> AdvisedEmployees { get; set; } = null!;
}
