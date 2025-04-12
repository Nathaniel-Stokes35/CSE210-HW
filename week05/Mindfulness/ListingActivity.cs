public class ListingActivity : MindfulnessActivity
{
    public ListingActivity() 
        : base("Listing", "This activity helps quality of life and mood by focusing on positivity.") { }

    public void Run()
    {
        StartActivity();
        Console.WriteLine("List the good that's occured today, try for at least " + (_duration/5) + " seperate events, thoughts, or encounters:");
        List<string> responses = new List<string>();
        var endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            responses.Add(Console.ReadLine());
        }
        Console.WriteLine($"You listed {responses.Count} items!");
        if (responses.Count > _duration / 5)
        {
            Console.WriteLine("Great Job! You were able to successfully accomplish the Listing Activity; is there more positives you didn't mention? You could run this again with more time. And see how many you can get.");
        }
        EndActivity();
    }
}