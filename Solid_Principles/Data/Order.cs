namespace Solid_Principles.Data;

public class Order
{
    public int Id { get; set; }
    
    public List<Product> Products { get; set; }

    public Order()
    {
        Products = new List<Product>();
    }

    public double CalculateOrdersPrice()
    {
        return Products.Sum(p => p.Quantity * p.Price);
    }
    
}