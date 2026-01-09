public class Clicker
{
    public event Action<DateTime> Clicked;
    public event Action<int> TooManyClicks;
    public List<DateTime> Clicks { get; } = new();

    public Clicker()
    {
        Clicked += OnClicked;
    }

    public void Click()
    {
        DateTime clickTime = DateTime.Now;
        Clicked?.Invoke(clickTime);
    }

    private void OnClicked(DateTime clickTime)
    {
        Clicks.Add(clickTime);

        int clicksLastMinute = Clicks.Count(t => (clickTime - t).TotalSeconds <= 60);

        DateTime? firstClick = Clicks.OrderBy(t => t).FirstOrDefault();

        DateTime? lastClick = Clicks.OrderByDescending(t => t).FirstOrDefault();

        Console.WriteLine($"First click: {firstClick}");
        Console.WriteLine($"Last click: {lastClick}");

        if (clicksLastMinute > 10)
        {
            TooManyClicks?.Invoke(clicksLastMinute);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Clicker clicker = new Clicker();
        clicker.TooManyClicks += (count) =>
        {
            Console.WriteLine($"Warning: Too many clicks in the last minute! Count: {count}");
        };
        for (int i = 0; i < 12; i++)
        {
            clicker.Click();
            System.Threading.Thread.Sleep(5000);
        }
    }
}