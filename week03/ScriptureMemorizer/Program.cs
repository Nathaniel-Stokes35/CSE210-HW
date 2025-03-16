using System;
using System.Collections.Generic;

// This program exceeds expectations by allowing the user to memorize multiple scriptures in a single session.
// The user can also specify the percentage of the text to start with being shown.
// The user can see real-time feedback on how many words are being shown, for user metrics.
// The user can also choose to memorize another set of scriptures after completing the set, and the set can be as long as an entire book of scripture.
// The user can play as long as they wish on whatever level they desire for as long as they want, even with no hints or prompts on the screen

//***********------------------------------------IMPORTANT------------------------------------***********!!!!!!
// If you want the program to terminate when it hits 0% remaing shown words, remove the "//" from the first comment in the MemorizationGame.cs file.
// If you want the program to terminate when it hits 0% remaing shown words, remove the "//" from the first comment in the MemorizationGame.cs file.
// If you want the program to terminate when it hits 0% remaing shown words, remove the "//" from the first comment in the MemorizationGame.cs file.
//***********------------------------------------IMPORTANT------------------------------------***********!!!!!!

class Program
{
    static void Main(string[] args)
    {
        int _count = 0;

        while (true)
        {
            ScriptureLibrary library = new ScriptureLibrary();
            Console.WriteLine("What Books, Chapters, or Verses are you trying to memorize? (e.g 1 Nephi 3:7-10)");
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
