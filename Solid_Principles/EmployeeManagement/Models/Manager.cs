namespace Solid_Principles.EmployeeManagement.Models;

public class Manager : Employee
{
    public Manager(int id, string name, double baseSalary) : base(id, name, baseSalary)
    {
    }

    public override double CalculateSalary()
    {
        return BaseSalary + (BaseSalary * 0.25);
    }
}
