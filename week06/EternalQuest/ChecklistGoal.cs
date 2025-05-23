using System.Diagnostics;

public class ChecklistGoal : Goal
{
    public ChecklistGoal(string filePath, string name, string description, int points, List<Activity> activities, bool isRepeatable = false)
        : base(filePath, name, description, "Checklist", points, activities, isRepeatable)
    {}
    public override int MarkComplete(Activity activity)
    {
        if (!_storedActivites.Contains(activity))
        {
            Console.WriteLine($"Activity '{activity.GetName()}' is not in the checklist.");
            return 0;
        }

        if (activity.IsActiveComplete())
        {
            Console.WriteLine($"Activity '{activity.GetName()}' is already completed.");
            return 0;
        }

        if (activity is ChecklistActivity checklist)
        {
            if (checklist.GetCurrentIteration() == checklist.GetIterations())
            {
                Console.WriteLine("");
                Console.WriteLine($"Congratulations on completing {checklist.GetName()}!");
                Console.WriteLine("Would you like to increase the number of iterations? (y/n)");
                string response = Console.ReadLine();
                if (response?.ToLower() == "y")
                {
                    Console.WriteLine("How many should we increase the number by?");
                    if (int.TryParse(Console.ReadLine(), out int increase))
                    {
                        checklist.SetIterations(checklist.GetIterations() + increase);
                        Console.WriteLine($"Activity '{checklist.GetName()}' iterations increased to {checklist.GetIterations()}.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid number. No changes made.");
                    }
                }
                else
                {
                    checklist.MarkComplete();
                }

                return checklist.GetPoints();
            }
            else
            {
                checklist.MarkComplete();
            }
        }
        else
        {
            _earnedPoints = activity.MarkComplete(); // Fallback for other activity types
        }

        Console.WriteLine($"Marking activity '{activity.GetName()}' as complete in {_name} checklist.");
        return activity.GetPoints();
    }
    public void ListActivities()
    {
        Console.WriteLine($"Activities for checklist '{_name}':");
        foreach (var activity in _storedActivites)
        {
            Console.WriteLine($"- {activity.GetName()}: {(activity.IsActiveComplete() ? "Complete" : "Incomplete")}");
        }
    }
    public void AddActivity(ChecklistActivity activity)
    {
        if (_storedActivites.Contains(activity))
        {
            Console.WriteLine($"Activity '{activity.GetName()}' is already in the checklist.");
            Console.WriteLine("Would you like to increase the number of iterations until complete? (y/n)");
            string response = Console.ReadLine();
            if (response?.ToLower() == "y")
            {
                Console.WriteLine("How many should we increase the number by? (i.e. if you say 1 and there are 10 left, it will become 11 left)");
                int increase = int.Parse(Console.ReadLine());
                activity.SetIterations(activity.GetIterations() + increase);
                Console.WriteLine($"Activity '{activity.GetName()}' iterations increased to {activity.GetIterations()}.");
            }
            return;
        }
        _storedActivites.Add(activity);
        Console.WriteLine($"Activity '{activity.GetName()}' added to checklist '{_name}'.");
    }
    public override string ToString()
    {
        return $"Goal Name: {_name}, Description: {_description}, Points: {_bonusPoints}, Complete: {_isComplete}";
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
            if (activity is ChecklistActivity checklist)
            {
                Console.WriteLine($"{checklist.GetCurrentIteration()}/{checklist.GetIterations()} --  {checklist.GetName()} Points: {checklist.GetCurrPoints()}/{checklist.GetCompletePoints()} |");
            }
            else if (activity.IsActiveComplete())
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
            _name = lines[0].Replace("Name:", "").Trim();
            _description = lines[1].Replace("Description:", "").Trim();
            _bonusPoints = int.Parse(lines[2].Replace("Points:", "").Trim());
            _isComplete = bool.Parse(lines[3].Replace("Complete:", "").Trim());

            string activitiesLine = lines[5].Replace("Activities:", "").Trim();
            if (!string.IsNullOrWhiteSpace(activitiesLine))
            {
                string[] activityData = activitiesLine.Split('|', StringSplitOptions.RemoveEmptyEntries);

                _storedActivites.Clear();
                foreach (var activity in activityData)
                {
                    string[] actParts = activity.Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
                    string name = actParts[0].Trim();
                    int points = int.Parse(actParts[1].Split(':')[1].Trim());
                    bool complete = bool.Parse(actParts[2].Split(':')[1].Trim());

                    var act = new Activity(name, points, DateTime.Now);
                    if (complete) act.MarkComplete();
                    _storedActivites.Add(act);
                }
            }

            Console.WriteLine($"Checklist goal '{_name}' loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load checklist goal: {ex.Message}");
        }
    }
}