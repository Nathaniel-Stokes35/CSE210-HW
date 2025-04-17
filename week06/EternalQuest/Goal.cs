public abstract class Goal
{
    protected List<string> _types = new List<string> { "Simple", "Eternal", "Checklist" };
    protected List<Activity> _storedActivites = new List<Activity>();
    protected string _name;
    protected string _type;
    protected string _description;
    protected int _bonusPoints;
    protected int _earnedPoints;
    protected int _totalPoints;
    protected bool _isComplete;
    protected string _fileName;
    protected string _filePath;
    protected bool _repeatable;

    public Goal(string filepath, string name, string description, string type, int points, List<Activity> activities, bool repeatable = false)
    {
        _filePath = filepath;
        _repeatable = repeatable;
        _name = name;
        _description = description;
        _type = type;
        _bonusPoints = points;
        _fileName = name + "_goal.txt";
        _isComplete = false;
        if (activities == null)
        {
            _storedActivites = new List<Activity>();
        }
        else
        {
            _storedActivites = activities;
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
        _fileName = filename;
    }
    public virtual string GetFileName()
    {
        return _fileName;
    }
    public virtual void SetFilePath(string filepath)
    {
        _filePath = filepath + _fileName;
    }
    public virtual void SetName(string name)
    {
        _name = name;
    }
    public virtual void SetDescription(string description)
    {
        _description = description;
    }
    public virtual void SetRepeatable(bool repeatable)
    {
        _repeatable = repeatable;
    }
    public virtual string IsType()
    {
        return _type;
    }
    public virtual bool IsRepeatable()
    {
        return _repeatable;
    }
    public virtual void SetBonusPoints(int points)
    {
        _bonusPoints = points;
    }
    public virtual int GetBonusPoints()
    {
        return _bonusPoints;
    }
    public virtual int GetTotalPoints()
    {
        _totalPoints = _storedActivites.Sum(a => a.GetTotalPoints()) + _bonusPoints;
        return _totalPoints;
    }
    public virtual string GetName()
    {
        return _name;
    }
    public virtual string GetDescription()
    {
        return _description;
    }
    public virtual void AddActivity()
    {
        Console.Write("Enter activity name: ");
        string name = Console.ReadLine();
        Console.Write("Enter activity points: ");
        int points = int.Parse(Console.ReadLine());
        var newActivity = new Activity(name, points, DateTime.Now);
        _storedActivites.Add(newActivity);
    }
    public virtual void AddActivity(Activity activity)
    {
        _storedActivites.Add(activity);
    }
    public virtual void AddActivities(List<Activity> activities)
    {
        foreach (var activity in activities)
        {
            _storedActivites.Add(activity);
        }
    }
    public virtual void RemoveActivity(Activity activity)
    {
        if (_storedActivites.Contains(activity))
        {
            _storedActivites.Remove(activity);
            Console.WriteLine($"Removed activity: {activity.GetName()} from '{_name}' checklist.");
        }
        else
        {
            Console.WriteLine($"Activity: {activity.GetName()} not found in '{_name}' checklist.");
        }
    }
    public virtual List<Activity> GetActivities()
    {
        return _storedActivites;
    }
    public abstract int MarkComplete(Activity activity);
    public virtual int MarkComplete()
    {
        Console.WriteLine($"Congratulations! '{_name}' is complete!");
        _isComplete = true;
        _earnedPoints = CalculateEarnedPoints() + _bonusPoints;
        return _earnedPoints;
    }
    public virtual bool IsGoalComplete()
    {
        return _isComplete;
    }
    public virtual void LoadGoal(string data)
    {}
    public virtual int Evaluate()
    {
        int i = 0;
        foreach (var activity in _storedActivites)
        {
            if (activity.IsActiveComplete())
            {
                i++;
            }
        }
        if (i == _storedActivites.Count)
        {
            MarkComplete();    
        }
        Console.WriteLine($"Progress on {_name}: ");
        if (_isComplete)
        {
            Console.WriteLine($"Goal '{_name}' is complete! You earned {_bonusPoints} points.");
            return _earnedPoints + _bonusPoints;
        }
        else
        {
            ListUnfinished();
            Console.WriteLine($"Goal '{_name}' has {_storedActivites.Count - i} more activities to complete.");
        }
        return _earnedPoints;
    }
    public virtual void ListUnfinished()
    {
        Console.WriteLine($"Activities for goal '{_name}':");
        foreach (var activity in _storedActivites)
        {
            if (!activity.IsActiveComplete())
            {
                Console.WriteLine($"- {activity.GetName()}: Incomplete");
            }
        }
    }
    public virtual int CalculateEarnedPoints()
    {
        return _storedActivites.Sum(a => a.IsActiveComplete() ? a.GetPoints() : 0);
    }
}