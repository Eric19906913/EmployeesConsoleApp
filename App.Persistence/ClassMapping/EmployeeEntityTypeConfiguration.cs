using App.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName)
            .HasMaxLength(100);

        builder.Property(x => x.Dni)
            .HasMaxLength(10);

        builder.Property(x => x.Salary)
            .HasPrecision(10,2);

        builder.Property(x => x.Age);
    }
}