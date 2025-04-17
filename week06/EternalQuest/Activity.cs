public class Activity
{
    private string _Name;
    private DateTime _Date;
    private int _TotalPoints;
    private int _EarnedPoints;
    private bool _IsComplete;
    private bool _Repeatable;

    public Activity(string name, int points, DateTime date, bool repeatable = false)
    {
        _Name = name;
        _TotalPoints = points;
        _Date = date;
        _IsComplete = false;
        _Repeatable = repeatable;
    }
    public bool IsRepeatable()
    {
        return _Repeatable;
    }
    public void SetRepeatable(bool repeatable)
    {
        _Repeatable = repeatable;
    }
    public void SetName(string name)
    {
        _Name = name;
    }
    public void SetDate(DateTime date)
    {
        _Date = date;
    }
    public DateTime GetDate()
    {
        return _Date;
    }
    public void SetEarnedPoints(int points)
    {
        _EarnedPoints = points;
    }
    public string GetName()
    {
        return _Name;
    }
    public int GetPoints()
    {
        return _EarnedPoints;
    }
    public int GetTotalPoints()
    {
        return _TotalPoints;
    }
    public void DisplayPoints()
    {
        Console.WriteLine($"Activity: {_Name}, Points: {_EarnedPoints}/{_TotalPoints}");
    }
    public void MarkComplete()
    {
        if (_IsComplete)
        {
            Console.WriteLine($"Activity '{_Name}' is already complete.");
            return;
        }
        _IsComplete = true;
        _EarnedPoints = _TotalPoints;
    }
    public bool IsActiveComplete()
    {
        return _IsComplete;
    }
    public string ToCSV()
    {
        return $"{_Name},{_TotalPoints},{_Date},{_Repeatable}";
    }

    public static Activity FromCSV(string csv)
    {
        var parts = csv.Split(',');
        return new Activity(parts[0], int.Parse(parts[1]), DateTime.Parse(parts[2]), bool.Parse(parts[3]));
    }
    public void Evaluate()
    {
        if (_IsComplete)
        {
            Console.WriteLine($"Activity '{_Name}' is complete! You earned {_EarnedPoints} points.");
        }
        else
        {
            Console.WriteLine($"Activity '{_Name}' is not complete yet.");
        }
    }
}