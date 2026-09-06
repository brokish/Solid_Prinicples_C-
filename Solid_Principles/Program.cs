using Solid_Principles.Data;
using Solid_Principles.Services;
using Solid_Principles.EmployeeManagement.Models;
using Solid_Principles.EmployeeManagement.Interfaces;
using Solid_Principles.EmployeeManagement.Services;
using EmployeeRepositoryEmp = Solid_Principles.EmployeeManagement.Services.EmployeeRepository;
using SalaryServiceEmp = Solid_Principles.EmployeeManagement.Services.SalaryService;
using EmailServiceEmp = Solid_Principles.EmployeeManagement.Services.EmailService;


var builder = WebApplication.CreateBuilder(args);

// Swagger სერვისების რეგისტრაცია
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 1. Health Check
app.MapGet("/health", () => Results.Ok("Healthy"));

// 2. შეკვეთის გაფორმება და გადახდა Swagger-იდან
app.MapPost("/api/orders/checkout", (Order order, string paymentMethod, string email) =>
{
    // ჯამური თანხის გამოთვლა Order-ის შიდა ლოგიკით
    double totalPrice = order.CalculateOrdersPrice();

    // 1. გადახდის მეთოდის შერჩევა (OCP)
    IPayment payment = paymentMethod.ToLower() switch
    {
        "card" => new CardPayment(),
        "cash" => new CashPayment(),
        "bank" => new BankPayment(),
        _ => null
    };

    if (payment == null)
    {
        return Results.BadRequest("არასწორი გადახდის მეთოდი! აირჩიეთ: card, cash, ან bank.");
    }

    // გადახდის შესრულება
    payment.Pay(totalPrice);

    // 2. ბაზაში შენახვა (SRP)
    var repository = new OrderRepository();
    repository.saveOrders(order);

    // 3. იმეილის გაგზავნა (SRP)
    var emailService = new Solid_Principles.Services.EmailService();
    emailService.SendOrderConfirmation(order, email);

    // 4. რეპორტის დაბეჭდვა (SRP)
    var report = new OrderReport();
    report.PrintReport(order);

    return Results.Ok(new
    {
        Message = "შეკვეთა წარმატებით განხორციელდა!",
        OrderId = order.Id,
        TotalPrice = totalPrice,
        PaymentStatus = $"გადახდილია {paymentMethod}-ით"
    });
});

// ====================== თანამშრომელთა სისტემა (SOLID Principles) ======================

// DIP - ინტერფეისების დამოკიდებულება
IEmployeeRepository employeeRepository = new EmployeeRepositoryEmp();
ISalaryService salaryService = new SalaryServiceEmp();
IEmailService emailService = new EmailServiceEmp();
var employeeReport = new EmployeeReport(employeeRepository, salaryService);

// 3. თანამშრომელის დამატება (OCP - ხელფასის ტიპის ცვლილების გარეშე)
app.MapPost("/api/employees/add", (string type, int id, string name, double baseSalary) =>
{
    Employee employee = type.ToLower() switch
    {
        "developer" => new Developer(id, name, baseSalary),
        "manager" => new Manager(id, name, baseSalary),
        "accountant" => new Accountant(id, name, baseSalary),
        "designer" => new Designer(id, name, baseSalary),
        _ => null
    };

    if (employee == null)
    {
        return Results.BadRequest("არასწორი თანამშრომლის ტიპი! აირჩიეთ: developer, manager, accountant, ან designer.");
    }

    // SRP - რეპოზიტორი ზრდის მხოლოდ დამატებით
    employeeRepository.AddEmployee(employee);

    // SRP - იმეილი ზრდის მხოლოდ გაგზავნილი
    emailService.SendWelcomeEmail(employee);

    return Results.Ok(new
    {
        Message = "თანამშრომელი წარმატებით დამატებულია!",
        EmployeeId = employee.Id,
        EmployeeName = employee.Name,
        EmployeeType = employee.GetType().Name
    });
});

// 4. ხელფასის გამოთვლა 
app.MapGet("/api/employees/{id}/salary", (int id) =>
{
    var employee = employeeRepository.GetEmployeeById(id);

    if (employee == null)
    {
        return Results.NotFound($"თანამშრომელი ID: {id} ვერ მოიძებნა.");
    }

    // SRP - ხელფასის გამოთვლა
    double salary = salaryService.CalculateEmployeeSalary(employee);

    // SRP - იმეილის გაგზავნა
    emailService.SendSalaryNotification(employee, salary);

    return Results.Ok(new
    {
        EmployeeId = employee.Id,
        EmployeeName = employee.Name,
        EmployeeType = employee.GetType().Name,
        BaseSalary = employee.BaseSalary,
        CalculatedSalary = salary,
        BonusPercentage = employee.GetType().Name switch
        {
            "Developer" => "15%",
            "Manager" => "25%",
            "Accountant" => "20%",
            "Designer" => "18%",
            _ => "0%"
        }
    });
});

// 5. ყველა თანამშრომლის ინფორმაციის ჩვენება
app.MapGet("/api/employees/all", () =>
{
    var employees = employeeRepository.GetAllEmployees();

    if (employees.Count == 0)
    {
        return Results.NotFound("თანამშრომელი ვერ მოიძებნა.");
    }

    var result = employees.Select(e => new
    {
        EmployeeId = e.Id,
        EmployeeName = e.Name,
        EmployeeType = e.GetType().Name,
        BaseSalary = e.BaseSalary,
        CalculatedSalary = e.CalculateSalary()
    });

    return Results.Ok(result);
});

// 6. თანამშრომელის წაშლა
app.MapDelete("/api/employees/{id}/remove", (int id) =>
{
    var employee = employeeRepository.GetEmployeeById(id);

    if (employee == null)
    {
        return Results.NotFound($"თანამშრომელი ID: {id} ვერ მოიძებნა.");
    }

    // SRP - წაშლა
    employeeRepository.RemoveEmployee(id);

    return Results.Ok(new
    {
        Message = "თანამშრომელი წარმატებით წაშლილია!",
        DeletedEmployeeId = id,
        DeletedEmployeeName = employee.Name
    });
});

// 7. დეტალური რეპორტი
app.MapGet("/api/employees/report/all", () =>
{
    employeeReport.PrintAllEmployeesReport();
    return Results.Ok("რეპორტი წარმატებით დაბეჭდილია.");
});

// 8. ცალკეული თანამშრომლის რეპორტი
app.MapGet("/api/employees/{id}/report", (int id) =>
{
    employeeReport.PrintEmployeeSalaryInfo(id);
    return Results.Ok("რეპორტი წარმატებით დაბეჭდილია.");
});

app.Run();