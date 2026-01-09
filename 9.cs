public class Cart
{
    Dictionary<int, int> Product = new Dictionary<int, int>();
    public event Action CartChanged;

    public void Add(int productId, int qty)
    {
        if (Product.ContainsKey(productId))
        {
            Product[productId] += qty;
        }
        else
        {
            Product[productId] = qty;
        }
        CartChanged?.Invoke();
    }

    public void Remove(int productId)
    {
        if (Product.ContainsKey(productId)) {
                        Product.Remove(productId);
            CartChanged?.Invoke();
        }
    }

    public void Clear()
    {
        Product.Clear();
        CartChanged?.Invoke();
    }

    public int GetTotalItems()
    {
        return Product.Sum(x => x.Value);
    }

    public Dictionary<int, int> GetTop3Products()
    {
        return Product.OrderByDescending(p => p.Value)
            .Take(3)
            .ToDictionary(p => p.Key, p => p.Value);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Cart cart = new Cart();
        cart.CartChanged += () => Console.WriteLine("Cart has been updated.");
        cart.Add(1, 2);
        cart.Add(2, 3);
        cart.Add(1, 1);
        cart.Remove(2);
        Console.WriteLine($"Total items in cart: {cart.GetTotalItems()}");
        var topProducts = cart.GetTop3Products();
        Console.WriteLine("Top 3 products in cart:");
        foreach (var item in topProducts)
        {
            Console.WriteLine($"Product ID: {item.Key}, Quantity: {item.Value}");
        }
        cart.Clear();
    }
}