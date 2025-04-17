using System.ComponentModel.Design;
using System.Formats.Asn1;


// Listen to Me Nathan, this is what you need to do. You need to 


public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _earnedPoints;
    private string _dirPath;
    private User _user;

    public GoalManager()
    {
        Console.WriteLine("Write your User Name to Save Info Under (This will also be the directory name saved under 'Users' in this directory): ");
        string userName = Console.ReadLine().ToLower();
        Console.WriteLine("");

        _dirPath = Path.Combine("Users", userName);
        _user = new User(userName, _goals);
        LoadGoals();
    }
    public void ChangeUser()
    {
        Console.WriteLine("");
        Console.WriteLine("Write the User Name of who you want to switch too: ");
        string userName = Console.ReadLine().ToLower();
        Console.WriteLine("");

        _dirPath = Path.Combine("Users", userName);
        _goals = new List<Goal>();
        LoadGoals();
        _user = new User(userName, _goals);
    }
    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
        SaveGoals();
        Console.WriteLine($"Goal '{goal.GetName()}' added.");
    }

    public void RemoveGoal()
    {
        Console.Write("Enter goal name to remove: ");
        string name = Console.ReadLine();
        Console.WriteLine("");
        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Goal name cannot be empty.");
            return;
        }
        var goal = _goals.FirstOrDefault(g => g.GetName() == name);
        if (goal != null)
        {
            _goals.Remove(goal);
            Console.WriteLine($"Goal '{name}' removed.");
        }
        else
        {
            Console.WriteLine($"Goal '{name}' not found.");
        }
        SaveGoals();
    }
    public void RemoveActivity()
    {
        Console.Write("Enter goal name: ");
        string goalName = Console.ReadLine();
        var goal = _goals.FirstOrDefault(g => g.GetName() == goalName);
        if (goal == null)
        {
            Console.WriteLine($"Goal '{goalName}' not found.");
            return;
        }

        Console.Write("Enter activity name to remove: ");
        string activityName = Console.ReadLine();
        Console.WriteLine("");
        var activity = goal.GetActivities().FirstOrDefault(a => a.GetName() == activityName);
        if (activity != null)
        {
            goal.RemoveActivity(activity);
            Console.WriteLine($"Activity '{activityName}' removed from goal '{goalName}'.");
        }
        else
        {
            Console.WriteLine($"Activity '{activityName}' not found in goal '{goalName}'.");
        }
        SaveGoals();
    }
    public void AddActivity()
    {
        Console.Write("Enter goal name: ");
        string goalName = Console.ReadLine();
        var goal = _goals.FirstOrDefault(g => g.GetName() == goalName);
        if (goal == null)
        {
            Console.WriteLine($"Goal '{goalName}' not found.");
            return;
        }
        if (goal.IsType() == "Checklist")
        {
            Console.WriteLine("");
            Console.Writeline("Is this a Multiple Occassion Activity? (i.e. Going out to the Gym 10 times, etc.) (y/n):")
            answer = Console.ReadLine();
            if (answer.ToLower() == "y")
            {
                Console.WriteLine("Activity Name: ");
                string activityName = Console.ReadLine();
                Console.WriteLine("Activity Points: ");
                int activityPoints = int.Parse(Console.ReadLine());
                Console.WriteLine("Number of Occurences before Bonus:");
                int increments = int.Parse(Console.ReadLine());
                Console.WriteLine("Bonus Point Amount: ");
                int bonusPoints = int.Parse(Console.ReadLine());

                activity = new ChecklistActivity(activityName, activityPoints, bonusPoints, DateTime.Now, increments, null);
                goal.AddActivity(activity);
            }
            else
            {
                activity = new Activity();
                goal.AddActivity(activity);
            }
            Console.WriteLine($"Activity '{activity.GetName()}' added to goal '{goalName}'.");
            SaveGoals();
            return;
        }
        Console.Write("Enter name of the new activity: ");
        string activityName = Console.ReadLine();
        Console.Write("Enter the points of the new activity: ");
        int points = int.Parse(Console.ReadLine());
        var newActivity = new Activity(activityName, points, DateTime.Now);
        goal.AddActivity(newActivity);
        Console.WriteLine("");
        Console.WriteLine($"Activity '{activityName}' added to goal '{goalName}'.");
        SaveGoals();
    }
    public void LoadGoals()
    {
        try
        {
            string[] goalFiles = Directory.GetFiles(_dirPath, "*_goals.txt");
            Console.WriteLine($"Loading goals from {_dirPath}...");
            Goal goal = null;

            foreach (var file in goalFiles)
            {
                string type = "";
                string name = "";
                string description = "";
                int bonusPoints = 0;
                bool complete = false;
                bool repeat = false;
                bool active = false;

                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        if (line.StartsWith("Type:"))
                        {
                            type = line.Split(':')[1].Trim().Trim(',', ' ');
                            switch (type.ToLower())
                            {
                                case "checklist":
                                    goal = new ChecklistGoal(_dirPath, name, description, bonusPoints, null, repeat);
                                    break;
                                case "eternal":
                                    goal = new EternalGoal(_dirPath, name, description, bonusPoints, null, repeat);
                                    break;
                                default:
                                    goal = new SimpleGoal(_dirPath, name, description, bonusPoints, null, repeat);                                
                                    break;
                            }
                        }
                        else if (line.StartsWith("Name:"))
                        {
                            name = line.Split(':')[1].Trim().Trim(',');
                            goal.SetName(name);

                        }
                        else if (line.StartsWith("Description:"))
                        {
                            description = line.Split(':')[1].Trim().Trim(',');
                            goal.SetDescription(description);
                        }
                        else if (line.StartsWith("Points:"))
                        {
                            bonusPoints = int.Parse(line.Split(':')[1].Trim().Trim(','));
                            goal.SetBonusPoints(bonusPoints);
                        }
                        else if (line.StartsWith("Activities:"))
                        {
                            active = true;
                        }
                        else if (active == true)
                        {
                            string[] activityParts = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                            foreach (string act in activityParts)
                            {
                                string[] details = act.Split(',', StringSplitOptions.RemoveEmptyEntries);
                                string activityName = details[0].Trim();
                                int points = int.Parse(details[1].Split(':')[1].Split('/')[0].Trim());
                                int totalPoints = int.Parse(details[1].Split(':')[1].Split('/')[1].Trim());
                                bool isComplete = bool.Parse(details[2].Split(':')[1].Trim());

                                Activity activity = new Activity(activityName, totalPoints, DateTime.Now);
                                if (isComplete)
                                {
                                    activity.MarkComplete();
                                }
                                goal.AddActivity(activity);
                            }
                            active = false;
                        }

                        else if (line.StartsWith("Repeatable:"))
                        {
                            repeat = bool.Parse(line.Split(':')[1].Trim().Trim(',', ' '));
                            goal.SetRepeatable(repeat);
                        }
                        else if (line.StartsWith("Complete:"))
                        {
                            complete = bool.Parse(line.Split(':')[1].Trim(',', ' '));
                            if (complete)
                            {
                                goal.MarkComplete();
                            }
                        }
                        else if (line.StartsWith("--"))
                        {
                            if (goal != null)
                            {
                                _goals.Add(goal);
                                goal = null;
                            }
                        }
                    }
                }
            }
            foreach (var obj in _goals)
            {
                int empty = obj.GetTotalPoints();
            }
            Console.WriteLine("");
            Console.WriteLine("All goal files loaded successfully.");
        }
        finally
        {
            SaveGoals();
        }
    }
    public void DisplayUserData()
    {
        _user.DisplayUserData();
    }
    public void SaveGoals()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(_dirPath, $"{DateTime.Now:yyyyMMdd}_goals.txt")))
            {
                foreach (var goal in _goals)
                {
                    writer.WriteLine($"Type: {goal.IsType()},");
                    writer.WriteLine($"Name: {goal.GetName()},");
                    writer.WriteLine($"Description: {goal.GetDescription()},");
                    writer.WriteLine($"Points: {goal.GetBonusPoints()},");
                    writer.WriteLine($"Activities: ");
                    foreach (var activity in goal.GetActivities())
                    {
                        writer.Write($"{activity.GetName()}, Points: {activity.GetPoints()}/{activity.GetTotalPoints()}, Complete: {activity.IsActiveComplete()} |");
                    }
                    writer.WriteLine();
                    writer.WriteLine($"Repeatable: {goal.IsRepeatable()},");
                    writer.WriteLine($"Complete: {goal.IsGoalComplete()}");
                    writer.WriteLine("--------------------------------------------------");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("");
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }
    public void CreateGoal()
    {
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();

        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();

        Console.Write("Enter goal points: ");
        int points = int.Parse(Console.ReadLine());

        Console.Write("Enter goal type (Simple, Checklist, or Eternal): ");
        string type = Console.ReadLine();

        bool repeatable = false;
        if (type.ToLower() != "eternal")
        {
            Console.Write("Is this goal repeatable? (yes/no): ");
            string repeatableInput = Console.ReadLine();
            repeatable = repeatableInput.ToLower() == "yes";
        }
        List <Activity> emptyActivities = new List<Activity>();
        Goal goal = null;
        switch (type.ToLower())
        {
            case "simple":
                goal = new SimpleGoal(_dirPath, name, description, points, emptyActivities, repeatable);
                break;
            case "checklist":
                goal = new ChecklistGoal(_dirPath, name, description, points, emptyActivities, repeatable);
                break;
            case "eternal":
                goal = new EternalGoal(_dirPath, name, description, points, emptyActivities, true);
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }
        while (true)
        {
            Console.WriteLine("");
            Console.Write("Do you want to add an activity? (yes/no): ");
            string addActivity = Console.ReadLine();
            if (addActivity.ToLower() == "no")
                break;
            goal.AddActivity();
        }
        _goals.Add(goal);
        SaveGoals();
    }

    public void DisplayGoals()
    {
        foreach (var goal in _goals)
        {
            goal.Display();
        }
    }

    public void EvaluateAll()
    {
        _earnedPoints = 0;
        int i = 0;
        Console.WriteLine("");
        foreach (var obj in _goals)
        {
            List<Activity> activities = obj.GetActivities();
            foreach (var active in activities)
            {
                if (active.IsActiveComplete())
                {
                    i++;
                    _earnedPoints += active.GetPoints();
                }
            }
            if (i == activities.Count)
            {
                Console.WriteLine($"All activities in goal '{obj.GetName()}' are complete.");
                obj.MarkComplete();
                _earnedPoints += obj.GetBonusPoints();
            }
            else
            {
                Console.WriteLine($"Not all activities in goal '{obj.GetName()}' are complete.");
            }
            i = 0;
        }
        Console.WriteLine($"Total points earned: {_earnedPoints} out of {_goals.Sum(g => g.GetTotalPoints())}");
        Console.WriteLine("");
        SaveGoals();
        _user.SetPoints(_earnedPoints);
        _user.SaveUserData();
    }
    public void MarkComplete()
    {
        Console.Write("Enter goal name: ");
        string goal = Console.ReadLine();
        Console.WriteLine("");

        var foundGoal = _goals.FirstOrDefault(g =>
            string.Equals(g.GetName(), goal, StringComparison.OrdinalIgnoreCase));

        if (foundGoal == null)
        {
            Console.WriteLine($"Goal '{goal}' not found.");
            Console.WriteLine("");
            return;
        }

        var activities = foundGoal.GetActivities();
        if (activities == null || activities.Count == 0)
        {
            int earned = foundGoal.MarkComplete();
            _earnedPoints += earned;
            if (foundGoal.IsType() == "Eternal")
            {
                Console.WriteLine($"Eternal goal {goal} has no activities but is labeled Eternal. Would you like to repeat this goal?");
                string answer = Console.ReadLine();
                Console.WriteLine("");
                if (answer.ToLower() == "yes")
                {
                    foundGoal.SetRepeatable(true);
                    Console.WriteLine("Add activity to this goal?");
                    answer = Console.ReadLine();
                    if (answer.ToLower() == "yes")
                    {
                        foundGoal.AddActivity();
                    }
                }
                else
                {
                    foundGoal.SetRepeatable(false);
                }
            }
            SaveGoals();
            Console.WriteLine($"Goal '{goal}' marked complete. You earned {earned} points.");
            Console.WriteLine("");
            return;
        }

        Console.Write("Enter activity name: ");
        string user_activity = Console.ReadLine();

        var activity = activities.FirstOrDefault(a =>
            string.Equals(a.GetName(), user_activity, StringComparison.OrdinalIgnoreCase));

        if (activity == null)
        {
            Console.WriteLine($"Activity '{user_activity}' not found in goal '{goal}'.");
            Console.WriteLine("");
            return;
        }

        _earnedPoints += foundGoal.MarkComplete(activity);
        EvaluateAll();
    }
}