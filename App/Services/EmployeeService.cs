using App.Domain;
using App.Persistence;

namespace TP7HaasEric;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        this.repository = repository;
    }

    public async Task AddEmployeeAsync(EmployeeDto employee)
    {
        var newEmployee = new Employee()
        {
            FullName = employee.FullName,
            Dni = employee.DNI,
            IsMarried = employee.IsMarried,
            Salary = employee.Salary,
            Age = employee.Age,
        };

        await this.repository.CreateAsync(newEmployee);
    }

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await this.repository.GetAllAsync();
    }
}
