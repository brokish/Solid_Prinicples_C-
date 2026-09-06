namespace Solid_Principles.EmployeeManagement.Interfaces;

using Models;

public interface IEmployeeRepository
{
    void AddEmployee(Employee employee);
    void RemoveEmployee(int id);
    Employee GetEmployeeById(int id);
    List<Employee> GetAllEmployees();
}
