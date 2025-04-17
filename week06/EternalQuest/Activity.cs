public class Activity
{
    private string _name;
    private DateTime _date;
    private int _totalPoints;
    private int _earnedPoints;
    private bool _isComplete;
    private bool _repeatable;

    public Activity(string name, int points, DateTime date, bool repeatable = false)
    {
        _name = name;
        _totalPoints = points;
        _date = date;
        _isComplete = false;
        _repeatable = repeatable;
    }
    public bool IsRepeatable()
    {
        return _repeatable;
    }
    public void SetRepeatable(bool repeatable)
    {
        _repeatable = repeatable;
    }
    public void SetName(string name)
    {
        _name = name;
    }
    public void SetDate(DateTime date)
    {
        _date = date;
    }
    public DateTime GetDate()
    {
        return _date;
    }
    public void SetEarnedPoints(int points)
    {
        _earnedPoints = points;
    }
    public string GetName()
    {
        return _name;
    }
    public int GetPoints()
    {
        return _earnedPoints;
    }
    public int GetTotalPoints()
    {
        return _totalPoints;
    }
    public void DisplayPoints()
    {
        Console.WriteLine($"Activity: {_name}, Points: {_earnedPoints}/{_totalPoints}");
    }
    public void MarkComplete()
    {
        if (_isComplete)
        {
            Console.WriteLine($"Activity '{_name}' is already complete.");
            return;
        }
        _isComplete = true;
        _earnedPoints = _totalPoints;
    }
    public bool IsActiveComplete()
    {
        return _isComplete;
    }
    public string ToCSV()
    {
        return $"{_name},{_totalPoints},{_date},{_repeatable}";
    }
    public void Evaluate()
    {
        if (_isComplete)
        {
            Console.WriteLine($"Activity '{_name}' is complete! You earned {_earnedPoints} points.");
        }
        else
        {
            Console.WriteLine($"Activity '{_name}' is not complete yet.");
        }
    }
}