using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.Adapter.EntityConfigurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired();

        builder.HasMany<Role>(e => e.RolesPreferred)
            .WithMany();
    }
}
