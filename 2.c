public class OrderEventArgs : EventArgs
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
}

public class Order
{
    public event EventHandler<OrderEventArgs>? OrderPlaced;

    public void CreateOrder(int orderId, decimal amount)
    {
        Console.WriteLine($"Order {orderId} created with amount {amount}.");
        OnOrderPlaced(new OrderEventArgs { OrderId = orderId, Amount = amount });
    }

    protected virtual void OnOrderPlaced(OrderEventArgs e)
    {
        OrderPlaced?.Invoke(this, e);
    }
}

public class Accountant
{
    public void OnOrderPlaced(object? sender, OrderEventArgs e)
    {
        Console.WriteLine($"Accountant notified: Order {e.OrderId} placed with amount {e.Amount}.");
    }
}

public class EmailService
{
    public void OnOrderPlaced(object? sender, OrderEventArgs e)
    {
        Console.WriteLine($"Email sent: Order {e.OrderId} has been successfully placed.");
    }
}

public class Program
{
    public static void Main()
    {
        Order order = new Order();
        Accountant accountant = new Accountant();
        EmailService emailService = new EmailService();

        order.OrderPlaced += accountant.OnOrderPlaced;
        order.OrderPlaced += emailService.OnOrderPlaced;

        order.CreateOrder(1, 99.99m);
    }
}