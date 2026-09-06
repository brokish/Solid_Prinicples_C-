namespace Solid_Principles.EmployeeManagement.Models;

public abstract class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double BaseSalary { get; set; }

    protected Employee(int id, string name, double baseSalary)
    {
        Id = id;
        Name = name;
        BaseSalary = baseSalary;
    }

    public abstract double CalculateSalary();
}
