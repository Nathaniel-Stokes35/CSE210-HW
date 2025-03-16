using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class ScriptureLibrary
{
    private List<Scripture> _library;
    private List<Scripture> _scriptures = new List<Scripture>();
    private int _currScriptureIndex = 0;

    public ScriptureLibrary()
    {
        LoadLibrary("book-of-mormon.json");
        LoadLibrary("pearl-of-great-price.json");
        LoadLibrary("new-testament.json");
        LoadLibrary("old-testament.json");
    }

    public void LoadLibrary(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        var jsonDocument = JsonDocument.Parse(jsonString);

        foreach (var bookElement in jsonDocument.RootElement.GetProperty("books").EnumerateArray())
        {
            string bookName = bookElement.GetProperty("book").GetString();

            foreach (var chapterElement in bookElement.GetProperty("chapters").EnumerateArray())
            {
                int chapterNum = chapterElement.GetProperty("chapter").GetInt32();

                foreach (var verseElement in chapterElement.GetProperty("verses").EnumerateArray())
                {
                    int verseNum = verseElement.GetProperty("verse").GetInt32();
                    string text = verseElement.GetProperty("text").GetString();
                    string reference = $"{bookName} {chapterNum}:{verseNum}";

                    var scripture = new Scripture();
                    scripture.ReadScripture(reference, text);
                    _scriptures.Add(scripture);
                }
            }
        }
    }

    public List<Scripture> DefineLibrary(string input)
    {
        Console.WriteLine("You have made it into the DefineLibrary method.");
        (string book, string chapter, string verse) = InterpretInput(input);

        if (!string.IsNullOrEmpty(book))
        {
            _library = _scriptures.FindAll(s =>
                s.GetBook().Equals(book, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(chapter) && _library.Count > 0)
        {
            if (int.TryParse(chapter, out int chapterNum))
            {
                _library = _library.FindAll(s => s.GetChapter() == chapterNum);
            }
        }

        if (!string.IsNullOrEmpty(verse) && _library.Count > 0)
        {
            if (verse.Contains('-'))
            {
                var range = verse.Split('-');
                if (range.Length == 2 &&
                    int.TryParse(range[0], out int startVerse) &&
                    int.TryParse(range[1], out int endVerse))
                {
                    _library = _library.FindAll(s =>
                        s.GetVerse() >= startVerse && s.GetVerse() <= endVerse);
                }
            }
            else if (int.TryParse(verse, out int singleVerse))
            {
                _library = _library.FindAll(s =>
                    s.GetVerse() == singleVerse);
            }
        }
        _scriptures = _library;

        return _library;
    }

    private (string, string, string) InterpretInput(string input)
    {
        string book = "";
        string chapters = "";
        string verses = "";

        if (input.Contains(":"))
        {
            int splitIndex = input.IndexOf(":");
            int chapterIndex = input.LastIndexOf(' ', splitIndex);

            verses = input.Substring(splitIndex + 1);
            chapters = input.Substring(chapterIndex + 1, splitIndex - chapterIndex - 1);
            book = input.Substring(0, chapterIndex);
        }
        else if (input.Contains(" "))
        {
            int splitIndex = input.LastIndexOf(' ');
            chapters = input.Substring(splitIndex + 1);
            book = input.Substring(0, splitIndex);
        }
        else
        {
            book = input;
        }

        return (book, chapters, verses);
    }

    public Scripture GetNextScripture()
    {
        if (_currScriptureIndex < _scriptures.Count)
        {
            return _scriptures[_currScriptureIndex++];
        }
        return null;
    }

    public bool HasNextScripture()
    {
        return _currScriptureIndex < _scriptures.Count;
    }

    public List<Scripture> GetLibrary()
    {
        return _library;
    }
}