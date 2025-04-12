public class MindfulnessActivity
{
    protected string _name;
    protected string _description;
    protected int _duration;
    protected int _level;
    protected string _breathString;

    public MindfulnessActivity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public virtual void StartActivity()
    {
        Console.Clear();
        Console.WriteLine($"Starting: {_name}\n{_description}");
        Console.Write("Enter length of time on activity in seconds: ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Preparing activity, find your mindful space...");
        ShowSpinner(3);
    }

    public virtual void EndActivity()
    {
        Console.WriteLine($"You have completed the {_name} activity. Well done!");
        ShowSpinner(2);
    }

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write("/\b"); Thread.Sleep(250);
            Console.Write("--\b"); Thread.Sleep(250);
            Console.Write("\\\b"); Thread.Sleep(250);
            Console.Write("|\b"); Thread.Sleep(250);
        }
        Console.WriteLine();
    }
    protected void ShowBreath(string type, int seconds)
    {
        switch (type.ToLower())
        {
            case "in":
                _breathString = "";
                for (int i = 0; i < seconds; i++)
                {
                    Console.Clear();
                    Console.WriteLine($"Breathe in: ({_breathString})");
                    Thread.Sleep(1000);
                    _breathString += "-";
                }
                break;

            case "hold":
                Console.Clear();
                Console.WriteLine($"Hold: ({_breathString})");
                Thread.Sleep(seconds * 1000);
                break;

            case "out":
                int totalDashes = _breathString.Length;
                double interval = (double)seconds / totalDashes;

                for (int i = totalDashes; i >= 0; i--)
                {
                    Console.Clear();
                    Console.WriteLine($"Breathe out: ({_breathString.Substring(0, i)})");
                    Thread.Sleep((int)(interval * 1000));
                }
                break;
        }
    }
}