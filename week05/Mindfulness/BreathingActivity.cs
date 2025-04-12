public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() 
        : base("Breathing", "This activity will help you relax by guiding you through slow breathing.\nFollow the prompts as you breath.") { }

    public void Run()
    {
        Console.Write("Enter Breathing Activity level (1-3): ");
        var input = Console.ReadLine();
        if (!int.TryParse(input, out _level))
        {
            Console.Write("Input Must be a Whole Number. Defaulting to level 1.");
            _level = 1;
        }
        else
        {
            _level = int.Parse(input);
        }
        if (_level < 1 || _level > 3)
        {
            Console.Write("Activity level must be between 1 and 3. Defaulting to level 1.");
            _level = 1;
        }
        StartActivity();
        var endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            switch (_level)
            {
                case 1:
                    Console.WriteLine("Breathe in... 4 seconds");
                    ShowBreath("in", 4);
                    Console.WriteLine("Hold... 2 seconds");
                    ShowBreath("hold", 2);
                    Console.WriteLine("Breathe out... 6 seconds");
                    ShowBreath("out", 6);
                    break;
                case 2:
                    Console.WriteLine("Breathe in... 4 seconds");
                    ShowBreath("in", 4);
                    Console.WriteLine("Hold... 7 seconds");
                    ShowBreath("hold", 7);
                    Console.WriteLine("Breathe out... 8 seconds");
                    ShowBreath("out", 8);
                    break;
                case 3:
                    Console.WriteLine("Breathe in... 6 seconds");
                    ShowBreath("in", 6);
                    Console.WriteLine("Hold... 3 seconds");
                    ShowBreath("hold", 3);
                    Console.WriteLine("Breathe out... 9 seconds");
                    ShowBreath("out", 9);
                    break;
            }
        }
        EndActivity();
    }
}