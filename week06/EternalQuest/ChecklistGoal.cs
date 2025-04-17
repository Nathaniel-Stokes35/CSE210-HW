using System.Diagnostics;

public class ChecklistGoal : Goal
{
    public ChecklistGoal(string filePath, string name, string description, int points, List<Activity> activities, bool isRepeatable = false)
        : base(filePath, name, description, "Checklist", points, activities, isRepeatable)
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
    public override int MarkComplete(Activity activity)
    {
        if (!_StoredActivites.Contains(activity))
        {
            Console.WriteLine($"Activity '{activity.GetName()}' is not in the checklist.");
            return 0;
        }
        if (activity.IsActiveComplete())
        {
            Console.WriteLine($"Activity '{activity.GetName()}' is already completed.");
            return 0;
        }
        Console.WriteLine($"Marking activity '{activity.GetName()}' as complete in {_Name} checklist.");
        activity.MarkComplete();
        return activity.GetPoints();
    }
    public void ListActivities()
    {
        Console.WriteLine($"Activities for checklist '{_Name}':");
        foreach (var activity in _StoredActivites)
        {
            Console.WriteLine($"- {activity.GetName()}: {(activity.IsActiveComplete() ? "Complete" : "Incomplete")}");
        }
    }
    public override string ToString()
    {
        return $"Goal Name: {_Name}, Description: {_Description}, Points: {_BonusPoints}, Complete: {_IsComplete}";
    }
    public override void Display()
    {
        Console.WriteLine("");
        Console.WriteLine($"Type: {IsType()},");
        Console.WriteLine($"Name: {GetName()},");
        Console.WriteLine($"Description: {GetDescription()},");
        Console.WriteLine($"Points: {GetBonusPoints()},");
        Console.WriteLine($"Activities: ");
        foreach (var activity in GetActivities())
        {
            if (activity.IsActiveComplete())
            {
                Console.WriteLine($"[X] -- {activity.GetName()}, Points: {activity.GetPoints()}/{activity.GetTotalPoints()} |");
            }
            else
            {
                Console.WriteLine($"[ ] -- {activity.GetName()}, Points: {activity.GetPoints()}/{activity.GetTotalPoints()} |");
            }
        }
        Console.WriteLine($"Repeatable: {IsRepeatable()},");
        Console.WriteLine($"Complete: {IsGoalComplete()}");
        Console.WriteLine("");
        Console.WriteLine("--------------------------------------------------");
    }
    public override void LoadGoal(string filename)
    {
        try
        {
            string[] lines = File.ReadAllLines(filename);
            _Name = lines[0].Replace("Name:", "").Trim();
            _Description = lines[1].Replace("Description:", "").Trim();
            _BonusPoints = int.Parse(lines[2].Replace("Points:", "").Trim());
            _IsComplete = bool.Parse(lines[3].Replace("Complete:", "").Trim());

            string activitiesLine = lines[5].Replace("Activities:", "").Trim();
            if (!string.IsNullOrWhiteSpace(activitiesLine))
            {
                string[] activityData = activitiesLine.Split('|', StringSplitOptions.RemoveEmptyEntries);

                _StoredActivites.Clear();
                foreach (var activity in activityData)
                {
                    string[] actParts = activity.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
                    string name = actParts[0].Trim();
                    int points = int.Parse(actParts[1].Split(':')[1].Trim());
                    bool complete = bool.Parse(actParts[2].Split(':')[1].Trim());

                    var act = new Activity(name, points, DateTime.Now);
                    if (complete) act.MarkComplete();
                    _StoredActivites.Add(act);
                }
            }

            Console.WriteLine($"Checklist goal '{_Name}' loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load checklist goal: {ex.Message}");
        }
    }
}