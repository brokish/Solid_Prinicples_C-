namespace Solid_Principles.Services;

public class CardPayment : IPayment
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Paid {amount} GEL with card.");
    }
}