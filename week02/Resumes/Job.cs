class Job
{   public String _jobTitle;
    public String _company;
    public int _startYear;
    public int _endYear;
    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}