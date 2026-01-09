public enum EnemyType
{
    Goblin,
    Orc,
    Troll
}

public class Enemy
{
    public string Name { get; set; }
    public int HP { get; set; }
    public EnemyType Type { get; set; }

    public event Action<Enemy> Died;

    public void Die()
    {
        Died?.Invoke(this);
    }
}

public class BattleManager
{
    public List<Enemy> Enemies { get; } = new List<Enemy>();
    public List<Enemy> Killed { get; } = new List<Enemy>();

    public event Action WaveCleared;

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        Killed.Add(enemy);
        Enemies.Remove(enemy);
        if (Killed.Count > 10)
        {
            WaveCleared?.Invoke();
        }
    }

    public void CountKilledByType()
    {
        var counts = Killed.GroupBy(e => e.Type)
                           .ToDictionary(g => g.Key, g => g.Count());
        Console.WriteLine("Killed by type:");
        foreach (var kvp in counts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    public void FindMaxHpLiving()
    {
        var maxHpEnemy = Enemies.OrderByDescending(e => e.HP).FirstOrDefault();
        if (maxHpEnemy != null)
        {
            Console.WriteLine($"Enemy with max HP among living: {maxHpEnemy.Name}, HP: {maxHpEnemy.HP}");
        }
        else
        {
            Console.WriteLine("No living enemies.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BattleManager battleManager = new BattleManager();

        battleManager.WaveCleared += () =>
        {
            Console.WriteLine("Wave cleared! More than 10 enemies killed.");
        };

        Enemy enemy1 = new Enemy { Name = "Goblin1", HP = 50, Type = EnemyType.Goblin };
        Enemy enemy2 = new Enemy { Name = "Goblin2", HP = 60, Type = EnemyType.Goblin };
        Enemy enemy3 = new Enemy { Name = "Orc1", HP = 100, Type = EnemyType.Orc };
        Enemy enemy4 = new Enemy { Name = "Orc2", HP = 120, Type = EnemyType.Orc };
        Enemy enemy5 = new Enemy { Name = "Troll1", HP = 200, Type = EnemyType.Troll };
        Enemy enemy6 = new Enemy { Name = "Troll2", HP = 180, Type = EnemyType.Troll };
        Enemy enemy7 = new Enemy { Name = "Goblin3", HP = 55, Type = EnemyType.Goblin };
        Enemy enemy8 = new Enemy { Name = "Orc3", HP = 110, Type = EnemyType.Orc };
        Enemy enemy9 = new Enemy { Name = "Troll3", HP = 190, Type = EnemyType.Troll };
        Enemy enemy10 = new Enemy { Name = "Goblin4", HP = 65, Type = EnemyType.Goblin };
        Enemy enemy11 = new Enemy { Name = "Orc4", HP = 130, Type = EnemyType.Orc };
        Enemy enemy12 = new Enemy { Name = "Troll4", HP = 210, Type = EnemyType.Troll };
        Enemy enemy13 = new Enemy { Name = "Goblin5", HP = 70, Type = EnemyType.Goblin };

        battleManager.AddEnemy(enemy1);
        battleManager.AddEnemy(enemy2);
        battleManager.AddEnemy(enemy3);
        battleManager.AddEnemy(enemy4);
        battleManager.AddEnemy(enemy5);
        battleManager.AddEnemy(enemy6);
        battleManager.AddEnemy(enemy7);
        battleManager.AddEnemy(enemy8);
        battleManager.AddEnemy(enemy9);
        battleManager.AddEnemy(enemy10);
        battleManager.AddEnemy(enemy11);
        battleManager.AddEnemy(enemy12);
        battleManager.AddEnemy(enemy13);
        enemy1.Die();
        enemy2.Die();
        enemy3.Die();
        enemy4.Die();
        enemy5.Die();
        enemy6.Die();
        enemy7.Die();
        enemy8.Die();
        enemy9.Die();
        enemy10.Die();
        enemy11.Die();
        enemy12.Die();

        battleManager.CountKilledByType();
        battleManager.FindMaxHpLiving();
    }
}