namespace Solid_Principles.Services;

public class BankPayment : IPayment
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Paid {amount} GEL with bank.");
    }
}