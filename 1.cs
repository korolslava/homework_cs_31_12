public class UserService
{
    public event Action<string> UserLoggedIn;

    public void Login(string username)
    {
        Console.WriteLine($"{username} has logged in.");
        UserLoggedIn?.Invoke(username);
    }
}

public class LoginStats
{
    public int Count { get; set; } = 0;

    public void onLogin(string username)
    {
        Count++;
        Console.WriteLine($"Login count updated: {Count}");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        UserService userService = new UserService();
        LoginStats loginStats = new LoginStats();

        userService.UserLoggedIn += loginStats.onLogin;

        userService.Login("Alice");
        userService.Login("Bob");
        userService.Login("Charlie");
    }
}