namespace Solid_Principles.EmployeeManagement.Interfaces;

using Models;

public interface IEmailService
{
    void SendSalaryNotification(Employee employee, double salary);
    void SendWelcomeEmail(Employee employee);
}
