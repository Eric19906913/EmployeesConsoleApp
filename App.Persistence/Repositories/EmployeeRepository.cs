using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task CreateAsync(Employee employee)
    {
        await this.dbContext.Employees.AddAsync(employee);
        await this.SaveChangesAsync();
    }

    public Task<IEnumerable<Employee>> GetAllAsync()
    {
        return Task.FromResult(this.dbContext.Employees.AsEnumerable());
    }

    private async Task<bool> SaveChangesAsync()
    {
        // If more than 0 registered then return true.
        return await this.dbContext.SaveChangesAsync() > 0;
    }
}
