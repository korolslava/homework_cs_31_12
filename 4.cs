public class Enemy
{
    public string Name { get; set; }

    public Enemy(string name)
    {
        Name = name;
        EnemyList = new List<Enemy>();
        DeadEnemies = new List<Enemy>();
    }

    public event Action<bool> Died;

    List<Enemy> EnemyList;

    List<Enemy> DeadEnemies;

    public void Die()
    {
        DeadEnemies.Add(this);
        EnemyList.Remove(this);
        Died?.Invoke(true);
    }

    public void CountDeadEnemies()
    {
        Console.WriteLine($"Number of dead enemies: {DeadEnemies.Count()}");
    }

    public void FindFirstDeadEnemy()
    {
        var firstDeadEnemy = DeadEnemies.FirstOrDefault();
        
        if (firstDeadEnemy != null)
        {
            Console.WriteLine($"First dead enemy: {firstDeadEnemy.Name}");
        }
        else
        {
            Console.WriteLine("No dead enemies found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Enemy enemy1 = new Enemy("Goblin");
        Enemy enemy2 = new Enemy("Orc");
        Enemy enemy3 = new Enemy("Troll");
        enemy1.Died += (isDead) => 
        {
            if (isDead)
            {
                Console.WriteLine($"{enemy1.Name} has died.");
            }
        };
        enemy2.Died += (isDead) => 
        {
            if (isDead)
            {
                Console.WriteLine($"{enemy2.Name} has died.");
            }
        };
        enemy3.Died += (isDead) => 
        {
            if (isDead)
            {
                Console.WriteLine($"{enemy3.Name} has died.");
            }
        };
        enemy1.Die();
        enemy2.Die();
        enemy1.CountDeadEnemies();
        enemy1.FindFirstDeadEnemy();
    }
}