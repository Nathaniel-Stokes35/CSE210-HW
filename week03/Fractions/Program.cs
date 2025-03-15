using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction one = new Fraction();
        Fraction five = new Fraction(5);
        Fraction threeFourths = new Fraction(3, 4);
        Fraction oneThird = new Fraction(1, 3);

        Console.WriteLine(one.GetFractionsString());
        Console.WriteLine(one.GetDecimalValue());
        Console.WriteLine(five.GetFractionsString());
        Console.WriteLine(five.GetDecimalValue());
        Console.WriteLine(threeFourths.GetFractionsString());
        Console.WriteLine(threeFourths.GetDecimalValue());
        Console.WriteLine(oneThird.GetFractionsString());
        Console.WriteLine(oneThird.GetDecimalValue());
    }
}