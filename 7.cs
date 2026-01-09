using System.Reflection;

public class NotificationCenter
{
    public event Action <string> Notify;
    public void SendNotification(string message)
    {
        Notify?.Invoke(message);
    }

    public IEnumerable<MethodInfo> GetSubscribedMethods()
    {
        if (Notify == null)
            return Enumerable.Empty<MethodInfo>();
        return Notify.GetInvocationList()
                     .Select(d => d.Method);
    }
}

public class EmailService
{
    public void OnNotify(string message)
    {
        Console.WriteLine($"Email Service received notification: {message}");
    }
}

public class SMSService
{
    public void OnNotify(string message)
    {
        Console.WriteLine($"SMS Service received notification: {message}");
    }
}

public class PushService
{
    public void OnNotify(string message)
    {
        Console.WriteLine($"Push Service received notification: {message}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        NotificationCenter notificationCenter = new NotificationCenter();

        EmailService emailService = new EmailService();
        SMSService smsService = new SMSService();
        PushService pushService = new PushService();

        notificationCenter.Notify += emailService.OnNotify;
        notificationCenter.Notify += smsService.OnNotify;
        notificationCenter.Notify += pushService.OnNotify;

        notificationCenter.SendNotification("Test");

        var activeChannels = notificationCenter.GetSubscribedMethods();

        Console.WriteLine("\nActive Notification Channels");
        foreach (var method in activeChannels)
        {
            Console.WriteLine($"Channel: {method.DeclaringType?.Name} -> {method.Name}");
        }

        Console.WriteLine($"\nTotal active channels: {activeChannels.Count()}");
    }
}