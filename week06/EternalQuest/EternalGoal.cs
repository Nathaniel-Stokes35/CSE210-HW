public class EternalGoal : Goal
{
    public EternalGoal(string filePath, string name, string description, int points, List<Activity> activities, bool isRepeatable = true)
        : base(filePath, name, description, "Eternal", points, activities, isRepeatable)
    {
        if (activities == null)
        {
            _storedActivites = new List<Activity>();
        }
        else
        {
            _storedActivites = activities;
        }
    }
    public override int MarkComplete()
    {
        Console.WriteLine("");
        Console.WriteLine("Eternal Goals can never be complete, they can only be forgotten. ");
        return 0;
    }
    public override int MarkComplete(Activity activity)
    {
        if (activity.IsActiveComplete())
        {
            Console.WriteLine($"Activity '{activity.GetName()}' is already completed.");
            return 0;
        }
        Console.WriteLine($"Congratulations on finishing {activity.GetName()}! You received {activity.GetTotalPoints()} points.");
        activity.MarkComplete();
        return activity.GetPoints();
    }
    public override string ToString()
    {
        return $"Eternal Goal: {_name}, Description: {_description}, Points: {_bonusPoints}";
    }    
    public override void SetRepeatable(bool repeatable)
    {
        _repeatable = repeatable;
        if (repeatable)
        {
            Console.WriteLine($"Eternal goal '{_name}', and activities, will be repeated until forgotten.");
        }
        else
        {
            Console.WriteLine($"Eternal goal '{_name}', and associated activities, will be forgotten.");
        }
        foreach (var activity in _storedActivites)
        {
            activity.SetRepeatable(repeatable);
        }
    }
}