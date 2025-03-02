using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the percentage of your current grade (ex. 84, meaning 84%): ");
        string input = Console.ReadLine();
        float percent = float.Parse(input);
        int remain = (int)(percent % 10);
        string grade;
        string mod;
        if (percent >= 90) {
            grade = "A";
        }
        else if (percent >= 80) {
            grade = "B";
        }
        else if (percent >= 70) {
            grade = "C";
        }
        else if (percent >= 60) {
            grade = "D";
        }
        else {
            grade = "F";
        }
        if (grade == "F" || grade == "A") {
            if (percent <= 92 && grade != "F") {
                mod = "-";
            }
            else {
                mod = "";
            }
        }
        else {
            if (remain >= 7) {
                mod = "+";
            }
            else if (remain <= 2) {
                mod = "-";
            }
            else {
                mod = "";
            }
        }
        Console.WriteLine("Your grade is: " + grade + mod);
    }
}