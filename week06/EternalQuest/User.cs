using System.Net.Mail;

public class User
{
    private string _UserName;
    private string _FullName;
    private string _FirstName;
    private string _LastName;
    private DateTime _StartDate;
    private int _LongestStreak;
    private string _MostProgressType;
    private string _DirectoryPath;
    private int _EarnedPoints;
    private int _TotalPoints;
    private List<Goal> _Goals;

    public User(string username, List<Goal> goals)
    {
        _UserName = username.ToLower();
        _DirectoryPath = Path.Combine("Users", _UserName);
        _Goals = goals;

        if (!Directory.Exists(_DirectoryPath))
        {
            Console.WriteLine("No user profile found. Creating a new one...");
            Console.Write("Enter your first name: ");
            _FirstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            _LastName = Console.ReadLine();

            _StartDate = DateTime.Now;
            _LongestStreak = 0;
            _MostProgressType = "None";
            _FullName = $"{_FirstName} {_LastName}";
            _LongestStreak = 0;
            _MostProgressType = "None";
            
            Directory.CreateDirectory(_DirectoryPath);
            SaveUserData();
            Console.WriteLine($"User profile created for {_UserName}.");
        }
        else
        {
            string profilePath = Path.Combine(_DirectoryPath, "profile.txt");
            if (File.Exists(profilePath))
            {
                string[] lines = File.ReadAllLines(profilePath);

                _UserName = username;  
                _FullName = lines[1].Split(":")[1].Trim();
                if (_FullName.Contains(' '))
                {
                    _FirstName = _FullName.Split(' ')[0];
                    _LastName = _FullName.Split(' ')[1];
                }
                else
                {
                    _FirstName = _FullName;
                    _LastName = "";
                }
                _StartDate = DateTime.Parse(lines[2].Split(":")[1].Trim());
                _LongestStreak = int.Parse(lines[3].Split(":")[1].Trim());
                _MostProgressType = lines[4].Split(":")[1].Trim();
            }
        }
    }
    public User(){}
    public void SetPoints(int points)
    {
        _EarnedPoints = points;
    }
    public void SetTotalPoints(int points)
    {
        _TotalPoints = points;
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
        string path = Path.Combine(_DirectoryPath, "profile.txt");
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine($"Username: {_UserName}");
            writer.WriteLine($"Name: {_FirstName} {_LastName}");
            writer.WriteLine($"StartDate: {_StartDate.ToString("yyyy-MM-dd")}");
            writer.WriteLine($"LongestStreak: {_LongestStreak}");
            writer.WriteLine($"MostProgressType: {_MostProgressType}");
            writer.WriteLine($"Earned Points from Avaliable: {_EarnedPoints}");
            writer.WriteLine($"Total Points Avaliable: {_TotalPoints}");
        }
    }
    public void LoadUserData(string userName)
    {
        string path = Path.Combine(_DirectoryPath, "profile.txt");
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
                    case "Username": _UserName = value; break;
                    case "Name": _FullName = value; break;
                    case "StartDate": _StartDate = DateTime.Parse(value); break;
                    case "LongestStreak": _LongestStreak = int.Parse(value); break;
                    case "MostProgressType": _MostProgressType = value; break;
                    case "Earned Points from Avaliable": _EarnedPoints = int.Parse(value); break;
                    case "Total Points Avaliable": _TotalPoints = int.Parse(value); break;
                    default: Console.WriteLine($"Unknown key: {key}"); break;
                }
                _FirstName = _FullName.Split(' ')[0];
                _LastName = _FullName.Split(' ')[1];
            }
        }
        _DirectoryPath = Path.Combine("Users", _UserName);
    }
    public void AddGoal(Goal goal)
    {
        _Goals.Add(goal);
    }
    public void RemoveGoal(Goal goal)
    {
        _Goals.Remove(goal);
    }
    public void DisplayGoals()
    {
        Console.WriteLine($"Goals for {_UserName}:");
        foreach (var goal in _Goals)
        {
            Console.WriteLine(goal.ToString());
        }
    }
    public void DisplayUserData()
    {
        Console.WriteLine($"User: {_UserName}");
        Console.WriteLine($"Name: {_FullName}");
        Console.WriteLine($"Longest Streak: {_LongestStreak}");
        Console.WriteLine($"Most Progress Type: {_MostProgressType}");
        Console.WriteLine($"Start Date: {_StartDate.ToShortDateString()}");
        Console.WriteLine($"Goals: {_Goals.Count}");
        foreach (var goal in _Goals)
        {
            Console.WriteLine($"- {goal.GetName()}");
        }
        Console.WriteLine($"Earned Points: {_EarnedPoints}");
        Console.WriteLine($"Total Points: {_TotalPoints}");
    }
}