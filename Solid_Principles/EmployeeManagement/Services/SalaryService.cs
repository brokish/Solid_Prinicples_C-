namespace Solid_Principles.EmployeeManagement.Services;

using Models;
using Interfaces;

public class SalaryService : ISalaryService
{
    public double CalculateEmployeeSalary(Employee employee)
    {
        return employee.CalculateSalary();
    }

    public void PrintSalaryReport(Employee employee)
    {
        double salary = CalculateEmployeeSalary(employee);
        Console.WriteLine($"ID: {employee.Id}");
        Console.WriteLine($"სახელი: {employee.Name}");
        Console.WriteLine($"ტიპი: {employee.GetType().Name}");
        Console.WriteLine($"ხელფასი: {salary:F2} ₾");
    }
}
