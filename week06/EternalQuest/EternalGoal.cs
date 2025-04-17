public class EternalGoal : Goal
{
    public EternalGoal(string filePath, string name, string description, int points, List<Activity> activities, bool isRepeatable = true)
        : base(filePath, name, description, "Eternal", points, activities, isRepeatable)
    {
        if (activities == null)
        {
            _StoredActivites = new List<Activity>();
        }
        else
        {
            _StoredActivites = activities;
        }
    }
    public override int MarkComplete()
    {
        Console.WriteLine("Eternal Goals can never be complete, they are forever. ");
        return 0;
    }
    public override int MarkComplete(Activity activity)
    {
        if (activity.IsActiveComplete())
        {
            Console.WriteLine($"Activity '{activity.GetName()}' is already completed.");
            return 0;
        }
        Console.WriteLine($"Congratulations on finishing {activity.GetName()}! You received {activity.GetPoints()} points.");
        activity.MarkComplete();
        return activity.GetPoints();
    }
    public override string ToString()
    {
        return $"Eternal Goal: {_Name}, Description: {_Description}, Points: {_BonusPoints}";
    }    public override void SetRepeatable(bool repeatable)
    {
        _Repeatable = repeatable;
        if (repeatable)
        {
            Console.WriteLine($"Eternal goal '{_Name}', and activities, will be repeated until forgotten.");
        }
        else
        {
            Console.WriteLine($"Eternal goal '{_Name}', and associated activities, will be forgotten.");
        }
        foreach (var activity in _StoredActivites)
        {
            activity.SetRepeatable(repeatable);
        }
    }
}