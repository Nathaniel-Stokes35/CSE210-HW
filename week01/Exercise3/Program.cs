using System;

class Program
{
    static void Main(string[] args)
    {
        Random rand = new Random();
        
        do {
            int count = 0;
            int number = rand.Next(1, 100);
            int guess = 0;
            
            do {
                Console.Write("Guess the number between 1 and 100: ");
                string input = Console.ReadLine();

                bool isValid = int.TryParse(input, out guess); 

                if (!isValid) {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                if (guess == number) {
                    Console.WriteLine("You guessed the number! It was " + number + "!");
                }
                else if (guess < number) {
                    Console.WriteLine("Higher");
                }
                else if (guess > number) {
                    Console.WriteLine("Lower");
                }
                count++;
            } while (guess != number);

            Console.WriteLine("It took you " + count + " tries to guess the number.");
            Console.Write("Would you like to play again? (yes/no): ");

            string play = Console.ReadLine();
            play = play.ToLower();
            play = play.Trim();

            if (play == "no" || play == "n" || play == "exit" || play == "quit" || play == "nope") {
                break;
            }
        } while (true);
    }
}