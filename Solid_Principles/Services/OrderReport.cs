using Solid_Principles.Data;

namespace Solid_Principles.Services;

public class OrderReport
{
    public void PrintReport(Order order)
    {

        foreach (var product in order.Products)
        {
            Console.WriteLine($"- {product.Name}: {product.Quantity} x {product.Price} GEL = {product.Quantity * product.Price} GEL");
        }
        
        Console.WriteLine($"Total Price: {order.CalculateOrdersPrice()} GEL");
    }
}