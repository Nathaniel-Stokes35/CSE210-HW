public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int lengthMinutes, int laps)
        : base(date, lengthMinutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        double meters = _laps * 50;
        return meters / 1000.0;
    }

    public override double GetSpeed()
    {
        return GetDistance() / (LengthMinutes / 60.0);
    }

    public override double GetPace()
    {
        return LengthMinutes / GetDistance();
    }
}