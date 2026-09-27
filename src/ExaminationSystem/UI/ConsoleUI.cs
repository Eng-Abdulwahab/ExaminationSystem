using System;

namespace ExaminationSystem.UI;

public static class ConsoleUI
{
    public static void ShowTitle()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═════════════════════════════════════════════╗");
        Console.WriteLine("║            E X A M   S T U D I O            ║");
        Console.WriteLine("║           OOP Examination System            ║");
        Console.WriteLine("╚═════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    public static void ShowHeader(string title)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("─ " + title.ToUpper() + " " + new string('\u2500', 50 - title.Length - 3));
        Console.ResetColor();
    }

    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ShowInfo(string label, string value)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("> " + label + ": " + value);
        Console.ResetColor();
    }

    public static string ReadText(string label)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("> " + label + ": ");
        Console.ResetColor();
        return Console.ReadLine() ?? string.Empty;
    }

    public static string ReadRequiredText(string label)
    {
        while (true)
        {
            string input = ReadText(label);

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            ShowError("Input cannot be empty. Try again.");
        }
    }

    public static int ReadInt(string label, int min, int max)
    {
        while (true)
        {
            string input = ReadText(label);

            if (int.TryParse(input, out int value) && value >= min && value <= max)
                return value;

            ShowError($"Enter a whole number from {min} to {max}.");
        }
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Press any key to exit...");
        Console.ResetColor();
        Console.ReadKey(true);
    }
}