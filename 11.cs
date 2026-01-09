public enum Status
{
    New,
    InProgress,
    Complete
}

public class TaskItem
{
    public int Id { get; set; }
    public String Title { get; set; }
    public Status status { get; set; }
    public DateTime Created { get; set; }
}

public class TaskManager
{
    List<TaskItem> tasks = new List<TaskItem>();

    public event Action<TaskItem> TaskAdded;
    public event Action<TaskItem> TaskStatusChanged;

    public void AddTask(TaskItem task)
    {
        tasks.Add(task);
        TaskAdded?.Invoke(task);
    }

    public void CountTasksByStatus()
    {
        var statusCounts = tasks.GroupBy(t => t.status)
                                .ToDictionary(g => g.Key, g => g.Count());
        foreach (var status in Enum.GetValues(typeof(Status)).Cast<Status>())
        {
            int count = statusCounts.ContainsKey(status) ? statusCounts[status] : 0;
            Console.WriteLine($"{status}: {count}");
        }
    }

    public void FindTheOldestNotCompletedTask()
    {
        var oldestTask = tasks.Where(t => t.status != Status.Complete)
                              .OrderBy(t => t.Id)
                              .FirstOrDefault();
        if (oldestTask != null)
        {
            Console.WriteLine($"Oldest not completed task: Id={oldestTask.Id}, Title={oldestTask.Title}, Status={oldestTask.status}");
        }
        else
        {
            Console.WriteLine("All tasks are completed.");
        }
    }

    public void GetDoneTasksSortedByCreationDate()
    {
        var doneTasks = tasks.Where(t => t.status == Status.Complete)
                             .OrderBy(t => t.Created);
        foreach (var task in doneTasks)
        {
            Console.WriteLine($"Id={task.Id}, Title={task.Title}, Created={task.Created}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();
        taskManager.TaskAdded += (task) => Console.WriteLine($"Task added: Id={task.Id}, Title={task.Title}, Status={task.status}");
        taskManager.TaskStatusChanged += (task) => Console.WriteLine($"Task status changed: Id={task.Id}, New Status={task.status}");
        taskManager.AddTask(new TaskItem { Id = 1, Title = "Task 1", status = Status.New, Created = DateTime.Now.AddDays(-5) });
        taskManager.AddTask(new TaskItem { Id = 2, Title = "Task 2", status = Status.InProgress, Created = DateTime.Now.AddDays(-3) });
        taskManager.AddTask(new TaskItem { Id = 3, Title = "Task 3", status = Status.Complete, Created = DateTime.Now.AddDays(-1) });
        taskManager.AddTask(new TaskItem { Id = 4, Title = "Task 4", status = Status.New, Created = DateTime.Now.AddDays(-4) });
        Console.WriteLine("\nCount of tasks by status:");
        taskManager.CountTasksByStatus();
        Console.WriteLine("\nOldest not completed task:");
        taskManager.FindTheOldestNotCompletedTask();
        Console.WriteLine("\nDone tasks sorted by creation date:");
        taskManager.GetDoneTasksSortedByCreationDate();
    }
}