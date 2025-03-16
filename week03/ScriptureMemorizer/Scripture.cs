using System.ComponentModel.DataAnnotations;

public class Scripture
{
    private string _book;
    private int _chapter;
    private int _verse;
    private string _text;

    public string GetBook()
    {
        return _book;
    }

    public void SetBook(string book)
    {
        _book = book;
    }

    public int GetChapter()
    {
        return _chapter;
    }

    public void SetChapter(int chapter)
    {
        _chapter = chapter;
    }

    public int GetVerse()
    {
        return _verse;
    }

    public void SetVerse(int verse)
    {
        _verse = verse;
    }

    public List<Word> GetWords()
    {
        return _text.Split(' ').Select(word => new Word(word)).ToList();
    }
    public string GetText()
    {
        return _text;
    }
    public void SetText(string text)
    {
        _text = text;
    }

    public static (string bookName, int chapter, int verse) SplitReference(string reference)
    {
        string bookName;
        int chapter;
        int verse;
        int firstNumberIndex;

        if (char.IsDigit(reference[0]))
        {
            firstNumberIndex = reference.Substring(1).IndexOfAny("0123456789".ToCharArray());
        }
        else
        {
            firstNumberIndex = reference.IndexOfAny("0123456789".ToCharArray());
        }
        bookName = reference.Substring(0, firstNumberIndex).Trim();
        int Length = reference.Length;
        string remainder = reference.Substring(firstNumberIndex).Trim();
        var chapterVerse = remainder.Split(':');

        chapter = int.Parse(chapterVerse[0]);
        verse = int.Parse(chapterVerse[1]);

        return (bookName, chapter, verse);
    }

    public void ReadScripture(string reference, string text)
    {
        var parts = SplitReference(reference);
        _book = parts.bookName;
        _chapter = parts.chapter;
        _verse = parts.verse;
        _text = text;
    }

    public override string ToString()
    {
        return $"{_book} {_chapter}:{_verse} - {_text}";
    }
}