using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Adapter.EntityConfigurations;

internal class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(e => new { e.ProjectId, e.RoleId });

        builder.HasOne<Project>()
            .WithMany(e => e.Positions)
            .HasForeignKey(e => e.ProjectId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(e => e.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Employee>()
            .WithMany(e => e.PositionsPickedFor)
            .HasForeignKey(e => e.PickedEmployeeId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany<Skill>()
            .WithMany();

        builder.HasMany<Employee>(e => e.AdvisedEmployees)
            .WithMany();
    }
}
