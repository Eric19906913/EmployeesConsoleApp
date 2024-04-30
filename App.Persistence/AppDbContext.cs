using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
    }

    public DbSet<Employee> Employees { get; set; }
}
