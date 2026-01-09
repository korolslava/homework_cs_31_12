public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Message> SendMesseges { get; set; }
}

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
}

public class ChatRoom
{
    Queue<Message> messages = new Queue<Message>();
    public event Action<Message> MessageReceived;

    public event Action<Message> SpamDetected;

    private bool IsSpam(Message message)
    {
        return message.Text != null && message.Text.IndexOf("spam", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public void SendMessage(string text)
    {
        var message = new Message
        {
            Id = messages.Count + 1,
            Text = text,
            Timestamp = DateTime.Now
        };
        messages.Enqueue(message);
        MessageReceived?.Invoke(message);

        if (IsSpam(message))
        {
            SpamDetected?.Invoke(message);
        }
    }

    public void GetTop5MostActiveUsers(List<User> users)
    {
        var topUsers = users
            .OrderByDescending(u => u.SendMesseges.Count)
            .Take(5)
            .ToList();
        Console.WriteLine("Top 5 Most Active Users:");
        foreach (var user in topUsers)
        {
            Console.WriteLine($"{user.Name} - Messages Sent: {user.SendMesseges.Count}");
        }
    }

    public void GetLast5MessagesFromUser(User user)
    {
        var lastMessages = user.SendMesseges
            .OrderByDescending(m => m.Timestamp)
            .Take(5)
            .ToList();
        Console.WriteLine($"Last 5 Messages from {user.Name}:");
        foreach (var message in lastMessages)
        {
            Console.WriteLine($"{message.Timestamp}: {message.Text}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var chatRoom = new ChatRoom();
        chatRoom.MessageReceived += (message) =>
        {
            Console.WriteLine($"New message received: {message.Text}");
        };
        chatRoom.SpamDetected += (message) =>
        {
            Console.WriteLine($"Spam detected: {message.Text}");
        };
        chatRoom.SendMessage("Hello everyone!");
        chatRoom.SendMessage("This is a spam message.");
        chatRoom.SendMessage("How are you all doing?");

        var users = new List<User>
        {
            new User { Id = 1, Name = "Alice", SendMesseges = new List<Message>
                {
                    new Message { Id = 1, Text = "Hi!", Timestamp = DateTime.Now.AddMinutes(-10) },
                    new Message { Id = 2, Text = "How's it going?", Timestamp = DateTime.Now.AddMinutes(-5) }
                }
            },
            new User { Id = 2, Name = "Bob", SendMesseges = new List<Message>
                {
                    new Message { Id = 3, Text = "Hello!", Timestamp = DateTime.Now.AddMinutes(-20) },
                    new Message { Id = 4, Text = "Anyone here?", Timestamp = DateTime.Now.AddMinutes(-15) },
                    new Message { Id = 5, Text = "Good morning!", Timestamp = DateTime.Now.AddMinutes(-1) }
                }
            }
        };

        chatRoom.GetTop5MostActiveUsers(users);
        chatRoom.GetLast5MessagesFromUser(users[1]);
    }
}