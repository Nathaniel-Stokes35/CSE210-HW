using System;
using System.Linq;

public class ReferenceSplitter
{
    public static (string bookName, int chapter, int verse) SplitReference(string reference)
    {
        var parts = reference.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        string bookName;
        int chapter;
        int verse;

        if (char.IsDigit(parts[0][0]))
        {
            bookName = parts[0] + " " + parts[1]; 
            parts = parts.Skip(2).ToArray();
        }
        else
        {
            bookName = parts[0];
            parts = parts.Skip(1).ToArray();
        }

        var chapterVerse = parts[0].Split(':');
        chapter = int.Parse(chapterVerse[0]);
        verse = int.Parse(chapterVerse[1]);

        return (bookName, chapter, verse);
    }
}
