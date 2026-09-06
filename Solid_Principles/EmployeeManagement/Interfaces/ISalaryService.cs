namespace Solid_Principles.EmployeeManagement.Interfaces;

using Models;

public interface ISalaryService
{
    double CalculateEmployeeSalary(Employee employee);
    void PrintSalaryReport(Employee employee);
}
