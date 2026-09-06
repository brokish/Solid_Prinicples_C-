using Solid_Principles.Data;

namespace Solid_Principles.Services;

public class OrderRepository
{
    public void saveOrders(Order order)
    {
        Console.WriteLine($"Saving order {order.Id}...");
    }
}