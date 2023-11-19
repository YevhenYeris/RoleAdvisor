using Microsoft.EntityFrameworkCore;
using RoleAdvisor.Adapter.EntityConfigurations;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Adapter;

public class RoleAdvisorContext : DbContext
{
    public RoleAdvisorContext(DbContextOptions<RoleAdvisorContext> options)
        : base(options)
    {

    }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Position> Positions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EmployeeConfigyration());
        builder.ApplyConfiguration(new SkillConfigyration());
        builder.ApplyConfiguration(new ProjectConfigyration());
        builder.ApplyConfiguration(new RoleConfigyration());
        builder.ApplyConfiguration(new PositionConfigyration());
    }
}
