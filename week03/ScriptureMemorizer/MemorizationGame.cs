using System;

public class MemorizationGame
{
    private string _originalScripture;
    Scripture _scripture;
    private List<Word> _words = new List<Word>();
    private int _difficulty;
    private List<Word> _visableWords = new List<Word>();
    private List<Word> _hiddenWords = new List<Word>();
    private Random _random = new Random();

    public MemorizationGame(Scripture scripture, int difficulty)
    {
        _scripture = scripture;
        _difficulty = difficulty;
        _words = scripture.GetWords();
        _originalScripture = string.Join(" ", _words.Select(word => word.GetDisplayText()));
        foreach (var word in _words)
        {
            _visableWords.Add(word);
        }
    }

    public Boolean Start()
    {
        Boolean _failed = false;
        Boolean _next = false;
        string hiddenScripture;
        hiddenScripture = HideWords(_difficulty);
        
        while (_next != true)
        {

            if (_difficulty == 0)
            {
                //Environment.Exit(0); // This is here simply if you want to ruin the assignment by killing the game when you've memorized it. This is only here for the grade because it is whats asked for on the assignment.
                Console.Clear();
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n"); 
                Console.WriteLine("Final Test: Can you recite the scripture from memory?");
                Console.Write("\nEnter the scripture from memory: ");
                string userAnswer = Console.ReadLine();
                if (userAnswer.Trim().ToLower() == "quit")
                {
                    Environment.Exit(0);
                }
                if (CheckAnswer(userAnswer))
                {
                    Console.WriteLine("\nSuccess! That is correct! You have successfully memorized this scripture!");
                }
                else
                {
                    Console.WriteLine("\nThat is incorrect, but don't give up.");
                    Console.WriteLine($"Correct answer: {_originalScripture}");
                    _failed = true;
                }

                Console.WriteLine("\nWould you like to:");
                Console.WriteLine("1.) Work with the next Scripture");
                Console.WriteLine("2.) Redo this Scripture");
                Console.WriteLine("3.) Quit");
                Console.Write("Input your selection: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _failed = false;
                        return _next = true;
                    case "2":
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please write the number corresponding to the selection you desire.");
                        break;
                }

                break; 
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Type 'Quit' to Exit\n{_difficulty}% Visable Scripture:\n{_scripture.GetBook()} {_scripture.GetChapter()}:{_scripture.GetVerse()}\n{hiddenScripture}\n");

                Console.Write("Enter the scripture from memory: ");
                string userAnswer = Console.ReadLine();
                if (userAnswer.Trim().ToLower() == "quit" || _difficulty == 0)
                {
                    Environment.Exit(0);
                }
                if (string.IsNullOrWhiteSpace(userAnswer))
                {
                    hiddenScripture = HideWord();
                    continue;
                }
                if (CheckAnswer(userAnswer))
                {
                    Console.WriteLine("\nSuccess! That is correct!");
                }
                else
                {
                    Console.WriteLine("\nThat is incorrect but don't give up.");
                    Console.WriteLine($"Correct answer: {_originalScripture}");
                    _failed = true;
                }

                Console.WriteLine("\nWould you like to:");
                Console.WriteLine("1.) Work with the next Scripture");
                if (_failed == true)
                {
                    Console.WriteLine("2.) Retry this scripture");
                }
                else
                {
                    Console.WriteLine("2.) Increase difficulty with this scripture");
                }
                Console.WriteLine("3.) Quit");
                Console.Write("Input your selection: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _failed = false;
                        return _next = true;
                    case "2":
                        if (_failed == true)
                        {
                            _failed = false;
                        }
                        else
                        {
                            _difficulty = Math.Min(_difficulty - 10, 5);
                        }
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please write the number corresponding to the selection you desire.");
                        break;
                }
            }
        }
        return _next = true;
    }
    private string HideWord()
    {
        int index = _random.Next(_visableWords.Count);
        Word wordToHide = _visableWords[index]; 
        _visableWords.RemoveAt(index); 
        _hiddenWords.Add(wordToHide); 
        wordToHide.Hide();  

        _difficulty = _visableWords.Count * 100 / _words.Count;

        Console.WriteLine($"Hiding word: {wordToHide.GetDisplayText()}");
        Console.WriteLine($"Remaining visible words: {_visableWords.Count}");
        Console.WriteLine($"Difficulty: {_difficulty}%");

        return string.Join(" ", _words.Select(word => word.GetDisplayText()));
    }

    private string HideWords(int percentToHide)
    {
        string[] words = _originalScripture.Split(' ');
        int wordsToHide = (int)(words.Length * percentToHide / 100.0);

        for (int i = 0; i < wordsToHide; i++)
        {
            HideWord();
        }

        return string.Join(" ", _words.Select(word => word.GetDisplayText()));
    }

    private bool CheckAnswer(string answer)
    {
        return answer.Trim().Equals(_originalScripture.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}