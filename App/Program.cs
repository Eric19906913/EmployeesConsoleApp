using App.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TP7HaasEric;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Use local db as sql server db.
builder.Services.AddDbContext<AppDbContext>(
    opts => opts.UseSqlServer(@"Server=(LocalDb)\MSSQLLocalDB;Database=TpHaasEric;Trusted_Connection=True;MultipleActiveResultSets=true"));
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

// Disable debug logs since we don't wanna see SQL queries and so.
builder.Logging.SetMinimumLevel(LogLevel.Error);

using IHost host = builder.Build();

// Apply migrations.
await host.RunMigrationsAsync();
await host.SeedInitialDataAsync();
await host.StartApplicationAsync();