namespace Solid_Principles.EmployeeManagement.Services;

using Models;
using Interfaces;

public class EmployeeRepository : IEmployeeRepository
{
    private static List<Employee> employees = new List<Employee>();

    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
        Console.WriteLine($"✓ თანამშრომელი {employee.Name} წარმატებით დამატებულია.");
    }

    public void RemoveEmployee(int id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee != null)
        {
            employees.Remove(employee);
            Console.WriteLine($"✓ თანამშრომელი {employee.Name} წარმატებით წაშლილია.");
        }
    }

    public Employee GetEmployeeById(int id)
    {
        return employees.FirstOrDefault(e => e.Id == id);
    }

    public List<Employee> GetAllEmployees()
    {
        return employees;
    }
}
