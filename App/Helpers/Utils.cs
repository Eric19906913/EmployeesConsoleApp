using App.Domain;
using ConsoleTables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Client;

namespace TP7HaasEric;

public static class Utils
{
    /// <summary>
    /// This is the entry point of our application, im forwarding the IHost to it
    /// to be able to use DI.
    /// </summary>
    /// <param name="host">The IHost.</param>
    public static async Task Menu(IHost host)
    {
        int option;

        do
        {
            Console.WriteLine("Elija una opción.");
            Console.WriteLine("1. Listar los empleados.");
            Console.WriteLine("2. Agregar un empleado.");
            Console.WriteLine("3. Salir.");
            
            Console.Write("Opción: ");
            Int32.TryParse(Console.ReadLine(), out option);


            switch (option)
            {
                case 1: Console.Clear(); 
                        await ListAllEmployees(host);
                    break;
                case 2: Console.Clear();
                        await RequestEmployeeInformation(host);
                    break;
                default: Console.Clear();
                    break;
            }

        } while (option != 3);
    }

    private static async Task RequestEmployeeInformation(IHost host)
    {
        Console.WriteLine("Ingresa la información del empleado.");

        string? name;
        string? dni;
        string? isMarried;

        do
        {
            Console.Write("Nombre completo: ");
            name = Console.ReadLine();

            if (name is null || name == string.Empty)
            {
                Console.WriteLine("Usted ingreso un valor invalido para el campo nombre.\n Intentelo nuevamente.");
            }
        } while (name is null || name == string.Empty);

        do
        {
            Console.Write("Dni: ");
            dni = Console.ReadLine();

            if (dni is null || dni == string.Empty)
            {
                Console.WriteLine("Usted ingreso un valor invalido para el campo dni.\n Intentelo nuevamente.");
            }
        } while (dni is null || dni == string.Empty);

        do
        {
            Console.Write("Casado S/N: ");
            isMarried = Console.ReadLine();

            if (isMarried is null || isMarried == string.Empty)
            {
                Console.WriteLine("Usted ingreso un valor invalido para el campo casado.\n Intentelo nuevamente.");
            }
        } while (isMarried?.ToUpper() != "S" && isMarried?.ToUpper() != "N");

        Console.Write("Edad: ");
        Int32.TryParse(Console.ReadLine(), out var age);

        Console.Write("Salario: ");
        decimal.TryParse(Console.ReadLine(), out var salario);

        var newEmployee = new EmployeeDto()
        {
            FullName = name.Trim(),
            DNI = dni.Trim(),
            Salary = salario,
            IsMarried = isMarried == "S",
            Age = age,
        };

        using (var scope = host.Services.CreateScope())
        {
            var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();

            await employeeService.AddEmployeeAsync(newEmployee);
        }

        Console.WriteLine("Presione una tecla para volver al menu.");
        Console.ReadKey();
        Console.Clear();
    }

    private static async Task ListAllEmployees(IHost host)
    {
        var employees = new List<Employee>();

        using (var scope = host.Services.CreateScope())
        {
            var employeeService = scope.ServiceProvider.GetRequiredService<IEmployeeService>();

            employees = (await employeeService.GetAllEmployeesAsync()).ToList();
        }

        var table = new ConsoleTable("Nombre", "Dni", "Casado", "Salario", "Edad");
        foreach (var employee in employees)
        {
            var married = employee.IsMarried ? "Si" : "No";
            table.AddRow(employee.FullName, employee.Dni, married, employee.Salary, employee.Age);
        }

        table.Write();
        Console.WriteLine("Presione una tecla para volver al menu.");
        Console.ReadKey();
        Console.Clear();
    }
}
