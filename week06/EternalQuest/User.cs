using System.Net.Mail;

public class User
{
    private string _userName;
    private string _fullName;
    private string _firstName;
    private string _lastName;
    private DateTime _startDate;
    private int _longestStreak;
    private string _mostProgressType;
    private string _directoryPath;
    private int _earnedPoints;
    private int _totalPoints;
    private List<Goal> _goals;

    public User(string username, List<Goal> goals)
    {
        _userName = username.ToLower();
        _directoryPath = Path.Combine("Users", _userName);
        _goals = goals;

        if (!Directory.Exists(_directoryPath))
        {
            Console.WriteLine("No user profile found. Creating a new one...");
            Console.Write("Enter your first name: ");
            _firstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            _lastName = Console.ReadLine();

            _startDate = DateTime.Now;
            _longestStreak = 0;
            _mostProgressType = "None";
            _fullName = $"{_firstName} {_lastName}";
            _longestStreak = 0;
            _mostProgressType = "None";
            
            Directory.CreateDirectory(_directoryPath);
            SaveUserData();
            Console.WriteLine($"User profile created for {_userName}.");
        }
        else
        {
            string profilePath = Path.Combine(_directoryPath, "profile.txt");
            if (File.Exists(profilePath))
            {
                string[] lines = File.ReadAllLines(profilePath);

                _userName = username;  
                _fullName = lines[1].Split(":")[1].Trim();
                if (_fullName.Contains(' '))
                {
                    _firstName = _fullName.Split(' ')[0];
                    _lastName = _fullName.Split(' ')[1];
                }
                else
                {
                    _firstName = _fullName;
                    _lastName = "";
                }
                _startDate = DateTime.Parse(lines[2].Split(":")[1].Trim());
                _longestStreak = int.Parse(lines[3].Split(":")[1].Trim());
                _mostProgressType = lines[4].Split(":")[1].Trim();
            }
        }
    }
    public User(){}
    public void SetPoints(int points)
    {
        _earnedPoints = points;
    }
    public void SetTotalPoints(int points)
    {
        _totalPoints = points;
    }
    private bool AreNamesSimilar(string saved, string entered)
    {
        saved = saved.ToLower().Trim();
        entered = entered.ToLower().Trim();

        return saved.StartsWith(entered) || LevenshteinDistance(saved, entered) <= 1;
    }
    private int LevenshteinDistance(string s, string t)
    {
        int[,] d = new int[s.Length + 1, t.Length + 1];

        for (int i = 0; i <= s.Length; i++) d[i, 0] = i;
        for (int j = 0; j <= t.Length; j++) d[0, j] = j;

        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 1; j <= t.Length; j++)
            {
                int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost
                );
            }
        }

        return d[s.Length, t.Length];
    }
    public void SaveUserData()
    {
        string path = Path.Combine(_directoryPath, "profile.txt");
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine($"Username: {_userName}");
            writer.WriteLine($"Name: {_firstName} {_lastName}");
            writer.WriteLine($"StartDate: {_startDate.ToString("yyyy-MM-dd")}");
            writer.WriteLine($"LongestStreak: {_longestStreak}");
            writer.WriteLine($"MostProgressType: {_mostProgressType}");
            writer.WriteLine($"Earned Points from Avaliable: {_earnedPoints}");
            writer.WriteLine($"Total Points Avaliable: {_totalPoints}");
        }
    }
    public void LoadUserData(string userName)
    {
        string path = Path.Combine(_directoryPath, "profile.txt");
        if (!File.Exists(path))
        {
            Console.WriteLine($"User profile for {userName} does not exist.");
            return;
        }
        string userData = File.ReadAllText(path);
        if (string.IsNullOrWhiteSpace(userData))
        {
            Console.WriteLine($"User profile for {userName} is empty.");
            return;
        }
        string[] lines = userData.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var parts = line.Split(':', 2);
            if (parts.Length == 2)
            {
                var key = parts[0].Trim();
                var value = parts[1].Trim().Trim(',');

                switch (key)
                {
                    case "Username": _userName = value; break;
                    case "Name": _fullName = value; break;
                    case "StartDate": _startDate = DateTime.Parse(value); break;
                    case "LongestStreak": _longestStreak = int.Parse(value); break;
                    case "MostProgressType": _mostProgressType = value; break;
                    case "Earned Points from Avaliable": _earnedPoints = int.Parse(value); break;
                    case "Total Points Avaliable": _totalPoints = int.Parse(value); break;
                    default: Console.WriteLine($"Unknown key: {key}"); break;
                }
                _firstName = _fullName.Split(' ')[0];
                _lastName = _fullName.Split(' ')[1];
            }
        }
        _directoryPath = Path.Combine("Users", _userName);
    }
    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }
    public void RemoveGoal(Goal goal)
    {
        _goals.Remove(goal);
    }
    public void DisplayGoals()
    {
        Console.WriteLine($"Goals for {_userName}:");
        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.ToString());
        }
    }
    public void DisplayUserData()
    {
        Console.WriteLine($"User: {_userName}");
        Console.WriteLine($"Name: {_fullName}");
        Console.WriteLine($"Longest Streak: {_longestStreak}");
        Console.WriteLine($"Most Progress Type: {_mostProgressType}");
        Console.WriteLine($"Start Date: {_startDate.ToShortDateString()}");
        Console.WriteLine($"Goals: {_goals.Count}");
        foreach (var goal in _goals)
        {
            Console.WriteLine($"- {goal.GetName()}");
        }
        Console.WriteLine($"Earned Points: {_earnedPoints}");
        Console.WriteLine($"Total Points: {_totalPoints}");
    }
}