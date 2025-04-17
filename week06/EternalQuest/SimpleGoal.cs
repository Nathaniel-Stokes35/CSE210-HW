public class SimpleGoal : Goal
{
    
    public SimpleGoal(string filePath, string name, string description, int points, List<Activity> activities, bool isRepeatable = false)
        : base(filePath, name, description, "Simple", points, activities, isRepeatable)
    { }
    public override int MarkComplete()
    {
        if (_isComplete)
        {
            Console.WriteLine($"Goal '{_name}' is already completed.");
            return 0;
        }

        Console.WriteLine($"Congratulations on completing the goal '{_name}'! You earned {_bonusPoints} points.");
        _isComplete = true;
        return _bonusPoints;
    }
    public override int MarkComplete(Activity activity)
    {
        
        if (activity.IsActiveComplete())
        {
            Console.WriteLine($"Activity '{activity.GetName()}' is already completed.");
            return 0;
        }
        activity.MarkComplete();
        Console.WriteLine($"Congratulations on finishing {activity.GetName()}! You received {activity.GetPoints()} points.");
        return activity.GetPoints();
    }
}