public class ChecklistActivity : Activity
{
    private string _name;
    private DateTime _date;
    private string _description;
    private int _bonusPoints;
    private int _stdPoints;
    private int _iterations;
    private int _currentIteration;
    private List<string> _checklistItems;
    private bool _isComplete;
    private bool _repeatable;

    public ChecklistActivity(string name, int bonus, int points, DateTime date, int iterations, List<string> checklistItems, bool repeatable = false)
        : base(name, points, date, repeatable)
    {
        _name = name;
        _bonusPoints = bonus;
        _stdPoints = points;
        _date = date;
        _iterations = iterations;
        _currentIteration = 0;
        _checklistItems = checklistItems ?? new List<string>();
        _isComplete = false;
        _repeatable = repeatable;
    }
    public void SetIterations(int iterations)
    {
        _iterations = iterations;
    }
    public void SetCurrentIteration(int currentIteration)
    {
        _currentIteration = currentIteration;
    }
    public int GetCurrentIteration()
    {
        return _currentIteration;
    }
    public int GetIterations()
    {
        return _iterations;
    }
    public void MarkComplete()
    {
        _currentIteration++;
        if (_currentIteration >= _iterations)
        {
            _isComplete = true;
        }
        else
        {
            Console.WriteLine($"{currentIteration}/{_iterations} times completed.");
            Console.WriteLine("");
            Console.WriteLine($"You have {_iterations - _currentIteration} times left before the bonus! You can do it!");
        }
    }
}