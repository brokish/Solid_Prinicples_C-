namespace Solid_Principles.Services;

public class CashPayment : IPayment
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Paid {amount} GEL in cash.");
    }
}