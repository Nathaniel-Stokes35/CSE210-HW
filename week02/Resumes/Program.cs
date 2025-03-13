using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job
        {
            _jobTitle = "Software Engineer",
            _company = "Microsoft",
            _startYear = 2017,
            _endYear = 2020
        };
        Job job2 = new Job
        {
            _jobTitle = "Software Engineer",
            _company = "Apple",
            _startYear = 2020,
            _endYear = 2021
        };
        Resume resume = new Resume
        {
            _name = "Nathaniel Stokes"
        };

        resume._jobs.Add(job1);
        resume._jobs.Add(job2);
        resume.Display();
    }
}