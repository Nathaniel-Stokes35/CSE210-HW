public class Party
{
    protected List<string> GuestList = new List<string>();
    protected int MaxGuests;

    public virtual void AddGuest(string guest)
    {
        GuestList.add(guest);
    }
    public virtual void RemoveGuest(string guest)
    {
        try
        {
            GuestList.remove(guest);
        }
        catch
        {
            Console.WriteLine("Guest not found.");
        }
    }
}
public class SnackTime : Party
{
    public SnackTime(int maxGuests)
    {
        MaxGuests = maxGuests;
        Console.WriteLine("Snack time begins, begin to mingle!");
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        SnackTime snackTime = new SnackTime(10);
        snackTime.AddGuest("Alice");
        snackTime.AddGuest("Bob");
        snackTime.RemoveGuest("Alice");
    }
}