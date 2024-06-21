namespace AlarmDisplay;

internal class Program
{
    private static void Main()
    {
        var alarmClient = new AlarmClient();
        Console.WriteLine("Alarm Display started successfully.");
        Console.WriteLine("Press [Enter] to close the program.");
        Console.WriteLine();
        Console.ReadLine();

        alarmClient.Close();
    }
}