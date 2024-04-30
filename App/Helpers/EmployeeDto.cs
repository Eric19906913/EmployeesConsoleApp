namespace TP7HaasEric;

/// <summary>
/// This is just a record to avoid using the domain class everywhere.
/// </summary>
public record EmployeeDto
{
    public string FullName { get; set; } = null!;

    public string DNI { get; set; } = null!;

    public bool IsMarried { get; set; }

    public decimal Salary { get; set; }

    public int Age { get; set; }
}
