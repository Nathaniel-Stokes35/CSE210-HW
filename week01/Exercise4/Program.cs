using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {   
        List<int> numberList = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished. ");
        int number = 0;
        do {
            Console.Write("Enter a number: ");
            bool isValid = int.TryParse(Console.ReadLine(), out number); 

            if (!isValid) {
                Console.WriteLine("Please enter a valid number.");
                continue;
            }
            if (number == 0) {
                break;
            }
            numberList.Add(number);
        } while (true);
        var positiveNumbers = numberList.Where(n => n > 0);

        Console.WriteLine("The sum is: " + numberList.Sum());
        Console.WriteLine("The average is: " + numberList.Average());
        Console.WriteLine("The largest number is: " + numberList.Max());
        Console.WriteLine("The smallest number is: " + numberList.Min());

        if (positiveNumbers.Any()) {
            Console.WriteLine("The smallest positive number is: " + positiveNumbers.Min());
        } else {
            Console.WriteLine("There are no positive numbers.");
        }
        numberList.Sort();
        Console.WriteLine("Your sorted list: " + string.Join(", ", numberList));
    }
}