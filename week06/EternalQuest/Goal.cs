public abstract class Goal
{
    protected List<string> _Types = new List<string> { "Simple", "Eternal", "Checklist" };
    protected List<Activity> _StoredActivites = new List<Activity>();
    protected string _Name;
    protected string _Type;
    protected string _Description;
    protected int _BonusPoints;
    protected int _EarnedPoints;
    protected int _TotalPoints;
    protected bool _IsComplete;
    protected string _FileName;
    protected string _FilePath;
    protected bool _Repeatable;

    public Goal(string filepath, string name, string description, string type, int points, List<Activity> activities, bool repeatable = false)
    {
        _FilePath = filepath;
        _Repeatable = repeatable;
        _Name = name;
        _Description = description;
        _Type = type;
        _BonusPoints = points;
        _FileName = name + "_goal.txt";
        _IsComplete = false;
        if (activities == null)
        {
            _StoredActivites = new List<Activity>();
        }
        else
        {
            _StoredActivites = activities;
        }
    }
    public virtual void Display()
    {
        Console.WriteLine("");
        Console.WriteLine($"Type: {IsType()},");
        Console.WriteLine($"Name: {GetName()},");
        Console.WriteLine($"Description: {GetDescription()},");
        Console.WriteLine($"Points: {GetBonusPoints()},");
        Console.WriteLine($"Activities: ");
        foreach (var activity in GetActivities())
        {
            Console.Write($"{activity.GetName()}, Points: {activity.GetPoints()}/{activity.GetTotalPoints()}, Complete: {activity.IsActiveComplete()} | ");
        }
        Console.WriteLine();
        Console.WriteLine($"Repeatable: {IsRepeatable()},");
        Console.WriteLine($"Complete: {IsGoalComplete()}");
        Console.WriteLine("");
        Console.WriteLine("--------------------------------------------------");
    }
    public virtual void SetFilename(string filename)
    {
        _FileName = filename;
    }
    public virtual string GetFileName()
    {
        return _FileName;
    }
    public virtual void SetFilePath(string filepath)
    {
        _FilePath = filepath + _FileName;
    }
    public virtual void SetName(string name)
    {
        _Name = name;
    }
    public virtual void SetDescription(string description)
    {
        _Description = description;
    }
    public virtual void SetRepeatable(bool repeatable)
    {
        _Repeatable = repeatable;
    }
    public virtual string IsType()
    {
        return _Type;
    }
    public virtual bool IsRepeatable()
    {
        return _Repeatable;
    }
    public virtual void SetBonusPoints(int points)
    {
        _BonusPoints = points;
    }
    public virtual int GetBonusPoints()
    {
        return _BonusPoints;
    }
    public virtual int GetTotalPoints()
    {
        _TotalPoints = _StoredActivites.Sum(a => a.GetTotalPoints()) + _BonusPoints;
        return _TotalPoints;
    }
    public virtual string GetName()
    {
        return _Name;
    }
    public virtual string GetDescription()
    {
        return _Description;
    }
    public virtual void AddActivity()
    {
        Console.Write("Enter activity name: ");
        string name = Console.ReadLine();
        Console.Write("Enter activity points: ");
        int points = int.Parse(Console.ReadLine());
        var newActivity = new Activity(name, points, DateTime.Now);
        _StoredActivites.Add(newActivity);
    }
    public virtual void AddActivity(Activity activity)
    {
        _StoredActivites.Add(activity);
    }
    public virtual void AddActivities(List<Activity> activities)
    {
        foreach (var activity in activities)
        {
            _StoredActivites.Add(activity);
        }
    }
    public virtual void RemoveActivity(Activity activity)
    {
        if (_StoredActivites.Contains(activity))
        {
            _StoredActivites.Remove(activity);
            Console.WriteLine($"Removed activity: {activity.GetName()} from '{_Name}' checklist.");
        }
        else
        {
            Console.WriteLine($"Activity: {activity.GetName()} not found in '{_Name}' checklist.");
        }
    }
    public virtual List<Activity> GetActivities()
    {
        return _StoredActivites;
    }
    public abstract int MarkComplete(Activity activity);
    public virtual int MarkComplete()
    {
        Console.WriteLine($"Congratulations! '{_Name}' is complete!");
        _IsComplete = true;
        _EarnedPoints = CalculateEarnedPoints() + _BonusPoints;
        return _EarnedPoints;
    }
    public virtual bool IsGoalComplete()
    {
        return _IsComplete;
    }
    public virtual void LoadGoal(string data)
    {}
    public virtual int Evaluate()
    {
        int i = 0;
        foreach (var activity in _StoredActivites)
        {
            if (activity.IsActiveComplete())
            {
                i++;
            }
        }
        if (i == _StoredActivites.Count)
        {
            MarkComplete();    
        }
        Console.WriteLine($"Progress on {_Name}: ");
        if (_IsComplete)
        {
            Console.WriteLine($"Goal '{_Name}' is complete! You earned {_BonusPoints} points.");
            return _EarnedPoints + _BonusPoints;
        }
        else
        {
            ListUnfinished();
            Console.WriteLine($"Goal '{_Name}' has {_StoredActivites.Count - i} more activities to complete.");
        }
        return _EarnedPoints;
    }
    public virtual void ListUnfinished()
    {
        Console.WriteLine($"Activities for goal '{_Name}':");
        foreach (var activity in _StoredActivites)
        {
            if (!activity.IsActiveComplete())
            {
                Console.WriteLine($"- {activity.GetName()}: Incomplete");
            }
        }
    }
    public virtual int CalculateEarnedPoints()
    {
        return _StoredActivites.Sum(a => a.IsActiveComplete() ? a.GetPoints() : 0);
    }
}