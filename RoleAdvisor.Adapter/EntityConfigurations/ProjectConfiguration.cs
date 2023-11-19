using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Adapter.EntityConfigurations;

internal class ProjectConfigyration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired();

        builder.HasOne<Employee>()
            .WithMany(e => e.ProjectsCreated)
            .HasForeignKey(e => e.CreatorId);
    }
}
