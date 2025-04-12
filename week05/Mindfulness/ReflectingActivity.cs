public class ReflectingActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time you overcame a challenge.",
        "Remember a moment of peace you had today.",
        "Recall a kind interaction with someone.",
        "Reflect on a time you felt proud of yourself.",
        "Consider a lesson you learned.",
        "Think of a time you added good to the world.",
        "Remember a moment of joy you experienced.",
        "Reflect on a time you felt grateful.",
        "Consider a time you made someone smile.",
        "Think of a time you felt connected to someone.",
        "Recall a moment of beauty you witnessed.",
        "Reflect on a time you felt inspired.",
        "Think of a time you felt loved.",
        "Remember a moment of laughter you shared.",
        "Think of a time you felt at peace."
    };
    private List<string> _reflect = new List<string>
    {
        "What did you learn from this experience?",
        "How did this experience change you?",
        "Would you change how you responded? Why or why not?",
        "Moving forward, how will you change because of this experience?",
        "What strengths did you discover in yourself?",
        "How can you use this experience to help others?",
        "What emotions did you feel during this experience?",
        "How can you carry this lesson forward?",
        "How would you use that moment to help someone caught in a similar situation?",
        "How would you share this moment with the world? Not just friends and family, but what would you want the world to take from it?"
    };

    public ReflectingActivity() 
        : base("Reflecting", "This activity will help you reflect on your day, building your strength and confidence to bolster through challenge and opposition. Helping you recognize you have power and that perception is reality.") { }

    public void Run()
    {
        StartActivity();
        var random = new Random();
        var endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            Console.WriteLine(_prompts[random.Next(_prompts.Count)]);
            ShowSpinner(5);
            Console.WriteLine("Now, to help you reflect: " + _reflect[random.Next(_reflect.Count)]);
            ShowSpinner(5);
        }
        EndActivity();
    }
}