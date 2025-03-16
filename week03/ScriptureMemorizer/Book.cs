public class Book
{
    private string _bookname;
    private List<Chapter> _chapters;

    public string GetBookName()
    {
        return _bookname;
    }

    public void SetBookName(string bookname)
    {
        _bookname = bookname;
    }

    public List<Chapter> GetChapters()
    {
        return _chapters;
    }

    public void SetChapters(List<Chapter> chapters)
    {
        _chapters = chapters;
    }
}