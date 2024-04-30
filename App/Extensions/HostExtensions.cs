using App.Domain;
using App.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;

namespace TP7HaasEric;

public static class HostExtensions
{
    public static async Task<IHost> StartApplicationAsync(this IHost host)
    {
        await Utils.Menu(host);

        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();

        return host;
    }

    public async static Task RunMigrationsAsync(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            Console.WriteLine("Corriendo migrations existentes...");
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.MigrateAsync();
            Console.WriteLine("Migrations aplicadas exitosamente...");
            Task.Delay(1000).Wait();
            Console.Clear();
        }
    }

    public async static Task SeedInitialDataAsync(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (dbContext.Employees.Count() > 1)
            {
                Console.WriteLine("Ya existe data creada, salteando seeding inicial...");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            string? option;

            do
            {
                Console.Write("Desea seedear data inicial? S/N: ");
                option = Console.ReadLine();

                if (option is null || option == string.Empty)
                {
                    Console.WriteLine("Usted ingreso un valor invalido para el campo casado.\n Intentelo nuevamente.");
                }
            } while (option?.ToUpper() != "S" && option?.ToUpper() != "N");


            if (option?.ToUpper() == "N")
            {
                Console.WriteLine("Salteando seeding inicial...");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("Creando data inicial...");
            await dbContext.Employees.AddRangeAsync(
                new List<Employee>
                {
                    new()
                    {
                        FullName = "Juan Topo",
                        Dni = "12345633",
                        Age = 55,
                        IsMarried = true,
                        Salary = 2500.02m,
                    },
                    new()
                    {
                        FullName = "Mirta Grand",
                        Dni = "100000",
                        Age = 85,
                        IsMarried = true,
                        Salary = 99989989.12m,
                    },
                    new()
                    {
                        FullName = "Marcos Pereira",
                        Dni = "36154214",
                        Age = 18,
                        IsMarried = false,
                        Salary = 10000m,
                    },
                    new()
                    {
                        FullName = "Josefina Fausto",
                        Dni = "54653756",
                        Age = 45,
                        IsMarried = true,
                        Salary = 1111152.45m,
                    },
                    new()
                    {
                        FullName = "Raul Portal",
                        Dni = "23152478",
                        Age = 65,
                        IsMarried = false,
                        Salary = 1000m,
                    },
                    new()
                    {
                        FullName = "Lizy Gaga",
                        Dni = "91235487",
                        Age = 35,
                        IsMarried = true,
                        Salary = 2556600.22m,
                    }
                });

            await dbContext.SaveChangesAsync();
            Console.WriteLine("Data inicial creada exitosamente...");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
