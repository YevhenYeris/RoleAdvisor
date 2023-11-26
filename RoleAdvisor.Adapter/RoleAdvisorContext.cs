using Microsoft.EntityFrameworkCore;
using RoleAdvisor.Adapter.EntityConfigurations;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Adapter;

/**
 * Add-Migration MigrationName -project RoleAdvisor.Adapter -startupproject RoleAdvisor.API -o Migrations
 * Update-Database -project RoleAdvisor.Adapter -startupproject RoleAdvisor.API
**/

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
        builder.ApplyConfiguration(new EmployeeConfiguration());
        builder.ApplyConfiguration(new SkillConfiguration());
        builder.ApplyConfiguration(new ProjectConfigyration());
        builder.ApplyConfiguration(new RoleConfigyration());
        builder.ApplyConfiguration(new PositionConfiguration());
    }
}
