namespace Solid_Principles.EmployeeManagement.Models;

public class Designer : Employee
{
    public Designer(int id, string name, double baseSalary) : base(id, name, baseSalary)
    {
    }

    public override double CalculateSalary()
    {
        return BaseSalary + (BaseSalary * 0.18);
    }
}
