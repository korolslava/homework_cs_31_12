public enum Departmets
{
    HR,
    IT,
    Sales
}

public class Employee
{
    public string Name { get; set; }
    public Departmets Departmet { get; set; }
    public decimal Salary { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee { Name = "Alice", Departmet = Departmets.IT, Salary = 60000 },
            new Employee { Name = "Bob", Departmet = Departmets.HR, Salary = 50000 },
            new Employee { Name = "Charlie", Departmet = Departmets.Sales, Salary = 70000 },
            new Employee { Name = "David", Departmet = Departmets.IT, Salary = 80000 },
            new Employee { Name = "Eve", Departmet = Departmets.HR, Salary = 55000 }
        };

        Func<Employee, bool> FilterStrategy;

        decimal X = 60000;
        FilterStrategy = emp => emp.Salary > X;
        var filteredBySalary = employees.Where(FilterStrategy).ToList();

        FilterStrategy = emp => emp.Departmet == Departmets.IT;
        var filteredByDepartment = employees.Where(FilterStrategy).ToList();

        Console.WriteLine("Employees with salary greater than " + X + ":");
        foreach (var employee in filteredBySalary)
        {
            Console.WriteLine(employee.Name);
        }

        Console.WriteLine("Employees in IT Department:");
        foreach (var employee in filteredByDepartment)
        {
            Console.WriteLine(employee.Name);
        }
    }
}