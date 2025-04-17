public class Cycling : Activity
{
    private double _speedKph;

    public Cycling(DateTime date, int lengthMinutes, double speedKph)
        : base(date, lengthMinutes)
    {
        _speedKph = speedKph;
    }

    public override double GetDistance() => _speedKph * (LengthMinutes / 60.0);

    public override double GetSpeed() => _speedKph;

    public override double GetPace() => 60.0 / _speedKph;
}