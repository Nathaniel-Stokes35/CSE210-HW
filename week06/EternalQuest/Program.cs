using System;
using System.Collections;

class Program
{
    static void Main(string[] args)
    {
        GoalManager _Manager = new GoalManager();
        Pause();
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Goal Tracker ===");
            Console.WriteLine("1. Create a New Goal");
            Console.WriteLine("2. Mark Activity Complete");
            Console.WriteLine("3. Display All Goals");
            Console.WriteLine("4. Display Progress");
            Console.WriteLine("5. Remove Goal");
            Console.WriteLine("6. Remove Activity");
            Console.WriteLine("7. Add Activity");
            Console.WriteLine("8. Display User Data");
            Console.WriteLine("9. Exit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    _Manager.CreateGoal();
                    _Manager.SaveGoals();
                    Pause();
                    break;
                case "2":
                    _Manager.MarkComplete();
                    Pause();
                    break;
                case "3":
                    _Manager.DisplayGoals();
                    Pause();
                    break;
                case "4":
                    _Manager.EvaluateAll();
                    Pause();
                    break;
                case "5":
                    _Manager.RemoveGoal();
                    Pause();
                    break;
                case "6":
                    _Manager.RemoveActivity();
                    Pause();
                    break;
                case "7":
                    _Manager.AddActivity();
                    Pause();
                    break;
                case "8":
                    _Manager.DisplayUserData();
                    Pause();
                    break;
                case "9":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    Pause();
                    break;
            }
        }
        Console.WriteLine("Goodbye!");
    }

    static void Pause()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}