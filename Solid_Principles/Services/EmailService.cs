using Solid_Principles.Data;

namespace Solid_Principles.Services;

public class EmailService
{
    public void SendOrderConfirmation(Order order, string email)
    {
        Console.WriteLine($"Sending order confirmation to {email} for order {order.Id}");
    }
}