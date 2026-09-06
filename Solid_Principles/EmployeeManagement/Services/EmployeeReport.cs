namespace Solid_Principles.EmployeeManagement.Services;

using Models;
using Interfaces;

public class EmployeeReport
{
    private readonly IEmployeeRepository _repository;
    private readonly ISalaryService _salaryService;

    public EmployeeReport(IEmployeeRepository repository, ISalaryService salaryService)
    {
        _repository = repository;
        _salaryService = salaryService;
    }

    public void PrintAllEmployeesReport()
    {
        var employees = _repository.GetAllEmployees();
        
        if (employees.Count == 0)
        {
            Console.WriteLine("❌ თანამშრომელი ვერ მოიძებნა.");
            return;
        }

        Console.WriteLine("\n📊 === თანამშრომელთა დეტალური რეპორტი ===");
        double totalSalary = 0;

        foreach (var employee in employees)
        {
            double salary = _salaryService.CalculateEmployeeSalary(employee);
            totalSalary += salary;
            Console.WriteLine($"ID: {employee.Id} | სახელი: {employee.Name} | ტიპი: {employee.GetType().Name} | ხელფასი: {salary:F2} ₾");
        }

        Console.WriteLine($"📈 ჯამური ხელფასი: {totalSalary:F2} ₾");
        Console.WriteLine($"👥 თანამშრომელთა რაოდენობა: {employees.Count}");
        Console.WriteLine("======================================\n");
    }

    public void PrintEmployeeSalaryInfo(int employeeId)
    {
        var employee = _repository.GetEmployeeById(employeeId);
        if (employee == null)
        {
            Console.WriteLine($"❌ ID: {employeeId} დ მქონე თანამშრომელი ვერ მოიძებნა.");
            return;
        }

        _salaryService.PrintSalaryReport(employee);
    }
}
