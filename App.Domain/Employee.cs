namespace App.Domain;

public class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Dni { get; set; } = null!;

    public bool IsMarried { get; set; }

    public decimal Salary { get; set; }

    public int Age { get; set; }
}