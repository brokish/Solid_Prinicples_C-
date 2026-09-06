namespace Solid_Principles.EmployeeManagement.Services;

using Models;
using Interfaces;

public class EmailService : IEmailService
{
    public void SendSalaryNotification(Employee employee, double salary)
    {
        Console.WriteLine($"ემეილი გაგზავნილია: {employee.Name}");
        Console.WriteLine($" თქვენი ხელფასი ამ თვეში {salary:F2} ₾");
    }

    public void SendWelcomeEmail(Employee employee)
    {
        Console.WriteLine($"მეილი გაგზავნილია: {employee.Name}");
    }
}
