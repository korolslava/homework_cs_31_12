public class TemperatureSensor
{
    public event Action<double> TemperatureChanged;

    List<double> temperatures = new List<double>();

    public void AddTemperature(double temperature)
    {
        temperatures.Add(temperature);
        TemperatureChanged?.Invoke(temperature);
        if (temperatures.Last() >= 30)
        {
            Console.WriteLine("Alarm: Temperature reached 30 degrees!");
        }
    }

    public void AvgTemperature()
    {
        var avgTemp = temperatures.Average();
        Console.WriteLine($"Average Temperature: {avgTemp}°C");
    }

    public void MaxTemperature()
    {
        var maxTemp = temperatures.Max();
        Console.WriteLine($"Maximum Temperature: {maxTemp}°C");
    }
}

class Program
{
    static void Main(string[] args)
    {
        TemperatureSensor sensor = new TemperatureSensor();
        sensor.TemperatureChanged += (temp) =>
        {
            Console.WriteLine($"New Temperature Recorded: {temp}°C");
        };
        sensor.AddTemperature(25.5);
        sensor.AddTemperature(28.3);
        sensor.AddTemperature(30.1);
        sensor.AddTemperature(27.8);

        sensor.AvgTemperature();
        sensor.MaxTemperature();
    }
}