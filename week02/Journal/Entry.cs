class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public void Display()
    {
        Console.WriteLine($"\nDate: {Date.ToShortDateString()}");
        Console.WriteLine($"Prompt: {Prompt}");
        Console.WriteLine($"Response: {Response}");
        Console.WriteLine("-------------------------------");
    }
}