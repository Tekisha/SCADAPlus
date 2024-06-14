using System;

namespace DatabaseManager.UI;

public class Menu
{
    public static void Show()
    {
        Console.WriteLine("1. Add new tag.");
        Console.WriteLine("2. Get tag value.");
        Console.WriteLine("3. Register new user.");
        Console.WriteLine("4. Log in.");
        Console.WriteLine("5. Exit.");
    }
}