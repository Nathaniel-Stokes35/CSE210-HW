using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        journal.WriteEntry();

        while (running)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");

            if (!string.IsNullOrEmpty(Journal.SaveLocation))
            {
                Console.WriteLine("6. Change Save Folder Location");
            }
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.WriteEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveJournal();
                    break;
                case "4":
                    Console.WriteLine("\nCurrent Save Folder: " + Journal.SaveLocation);
                    Console.WriteLine("Available files:");
                    if (Directory.Exists(Journal.SaveLocation))
                    {
                        string[] files = Directory.GetFiles(Journal.SaveLocation);
                        foreach (string file in files)
                        {
                            Console.WriteLine(Path.GetFileName(file));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failure: The Specified folder does not exist.");
                    }

                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    journal.UnsavedEntriesCheck();
                    journal.LoadFromFile(loadFile);
                    break;
                case "5":
                    journal.UnsavedEntriesCheck();
                    running = false;
                    break;
                case "6":
                    if (!string.IsNullOrEmpty(Journal.SaveLocation))
                    {
                        journal.SetSaveLocation();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}