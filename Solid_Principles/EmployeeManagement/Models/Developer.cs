namespace Solid_Principles.EmployeeManagement.Models;

public class Developer : Employee
{
    public Developer(int id, string name, double baseSalary) : base(id, name, baseSalary)
    {
    }

    public override double CalculateSalary()
    {
        return BaseSalary + (BaseSalary * 0.15);
    }
}
