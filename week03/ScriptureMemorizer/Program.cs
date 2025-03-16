using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int _count = 0;

        while (true)
        {
            ScriptureLibrary library = new ScriptureLibrary();
            Console.WriteLine("What Books, Chapters, or Verses are you trying to memorize? (e.g 1 Nephi 3:7)");
            string input = Console.ReadLine();
            if (input.Trim().ToLower() == "quit")
            {
                Console.WriteLine("Exiting the program...");
                break;
            }
            Console.WriteLine("What percentage of the text would you like to start with being shown? (1-100)");
            int startDifficulty = int.Parse(Console.ReadLine());
            Console.WriteLine("Thank you for your input. We will now search for the scriptures you are looking for.");

            var scriptureList = library.DefineLibrary(input);
            if (scriptureList.Count == 0)
            {
                Console.WriteLine("No scriptures found for the given input. Try again.");
                continue;
            }
            while (_count < scriptureList.Count)
            {
                Console.WriteLine("Scripture: " + scriptureList[_count].GetText());
                var scripture = scriptureList[_count];
                var game = new MemorizationGame(scripture, startDifficulty);
                bool next = game.Start();

                if (next)
                {
                    _count++;
                }
                else
                {
                    break; 
                }
            }
            Console.WriteLine("Would you like to memorize another set of scriptures? (y/n)");
            string retryInput = Console.ReadLine();
            if (retryInput.ToLower() != "y")
            {
                break;
            }
            _count = 0;
        }
    }
}
