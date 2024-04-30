using App.Domain;

namespace TP7HaasEric;

public interface IEmployeeService
{
    public Task AddEmployeeAsync(EmployeeDto employee);

    public Task<IEnumerable<Employee>> GetAllEmployeesAsync();
}