class Journal
{
    private List<Entry> _entries = new List<Entry>();
    private bool _isModified = false;
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "Did you have any regrets from your actions or are you happy with \"it was all apart of the plan\"?",
        "What do you think you'll remmeber most about today?",
        "What do you think was the most relatable, or familiar, part of your day?",
        "What did you learn? About yourself or Otherwise.",
        "What inspires you to write?"
    };

    public static string SaveLocation = LoadSaveLocation();
    public void WriteEntry()
    {
        Console.WriteLine("Would you like a prompt?\n1. Yes\n2. No");
        string choice = Console.ReadLine();
        string prompt = "";
        switch (choice)
        {
            case "1":
                Random random = new Random();
                prompt = _prompts[random.Next(_prompts.Count)];
                break;
            case "2":
                break;
            default:
                Console.WriteLine("Invalid Response - Record your Entry");
                break;
        }
                
        Console.WriteLine($"\n{prompt}");
        Console.Write("> ");
        string response = Console.ReadLine();

        Entry entry = new Entry
        {
            Prompt = prompt,
            Response = response,
            Date = DateTime.Now
        };

        _isModified = true;
        _entries.Add(entry);
        Console.WriteLine("Success: Entry saved!\n");
    }

    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("\nNo entries to display.");
            return;
        }

        Console.WriteLine("\nJournal Entries:");
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveJournal()
    {
        if (string.IsNullOrEmpty(SaveLocation))
        {
            Console.Write("Enter folder to save the journal (default: ../Entries): ");
            SaveLocation = Console.ReadLine();

            if (string.IsNullOrEmpty(SaveLocation))
            {
                SaveLocation = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Entries");
            }

            if (!Directory.Exists(SaveLocation))
            {
                Directory.CreateDirectory(SaveLocation);
            }

            SaveSaveLocation();
        }

        string fileName = Path.Combine(SaveLocation, "journal.txt");

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine($"{entry.Date.ToShortDateString()}|{entry.Prompt}|{entry.Response}");
            }
        }
        _isModified = false;
        _entries.Clear();
        Console.WriteLine($"Success: Journal saved to '{fileName}'");
    }
    public void LoadFromFile(string fileName)
    {
        string fullPath = fileName;

        if (!Path.IsPathRooted(fileName))
        {
            fullPath = Path.Combine(SaveLocation, fileName);
        }

        if (!File.Exists(fullPath))
        {
            Console.WriteLine("Failure: File not found.");
            return;
        }

        _entries.Clear();

        string[] lines = File.ReadAllLines(fullPath);
        foreach (string line in lines)
        {
            string[] parts = line.Split('|');
            if (parts.Length == 3)
            {
                Entry entry = new Entry
                {
                    Date = DateTime.Parse(parts[0]),
                    Prompt = parts[1],
                    Response = parts[2]
                };
                _entries.Add(entry);
            }
        }

        Console.WriteLine($"Success: Journal loaded from {fileName}");
    }
    public void SetSaveLocation()
    {
        Console.Write("Enter new folder to save the journal: ");
        string newLocation = Console.ReadLine();

        if (!string.IsNullOrEmpty(newLocation))
        {
            SaveLocation = newLocation;

            if (!Directory.Exists(SaveLocation))
            {
                Directory.CreateDirectory(SaveLocation);
            }

            SaveSaveLocation();
            Console.WriteLine($"Sucess: Save location set to '{SaveLocation}'");
        }
    }
    private static void SaveSaveLocation()
    {
        File.WriteAllText("save_config.txt", SaveLocation);
    }

    private static string LoadSaveLocation()
    {
        if (File.Exists("save_config.txt"))
        {
            return File.ReadAllText("save_config.txt").Trim();
        }
        return null;
    }

    public void UnsavedEntriesCheck()
    {
        if (_isModified && _entries.Count > 0)
        {
            Console.WriteLine("\nWARNING: You have Unsaved Entries. How would you like to handle them?");
            Console.WriteLine("1. Add Unsaved entries to the current journal");
            Console.WriteLine("2. Save unsaved entries to a different or new file");
            Console.WriteLine("3. Delete unsaved entries.");
            Console.Write("Choose your option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SaveToFile(SaveLocation);
                    Console.WriteLine("Success: Unsaved Changes have been added to the Current Journal.");
                    break;
                case "2":
                    Console.Write("Enter filename for unsaved entries: ");
                    string fileName = Console.ReadLine();
                    if (string.IsNullOrEmpty(fileName))
                    {
                        Console.WriteLine("Error: Invalid filename.");
                        break;
                    }
                    SaveToFile(fileName);
                    break;
                case "3":
                    _entries.Clear();
                    _isModified = false;
                    Console.WriteLine("Success: Unsaved entires deleted.");
                    break;
                default:
                    Console.WriteLine("Invalid Option. Unsaved entries retained.");
                    break;
            }
        }
    }
    private void SaveToFile(string fileName)
    {
        string filePath = Path.Combine(SaveLocation, fileName);
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine($"{entry.Date.ToShortDateString()}|{entry.Prompt}|{entry.Response}");
            }
        }
        _isModified = false;
        Console.WriteLine($"Success: Unsaved Entries saved to '{filePath}'");
    } 
}