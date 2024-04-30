using App.Domain;

namespace App.Persistence;

public interface IEmployeeRepository
{
    public Task CreateAsync(Employee employee);

    public Task<IEnumerable<Employee>> GetAllAsync();
}
