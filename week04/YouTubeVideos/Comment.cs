public class Comment
{
    public string _user;
    public string _text;
    public List<Comment> _replies;
    public DateTime _date;
    public Boolean _reply;

    public Comment(string user, string text)
    {
        _user = user;
        _text = text;
        _replies = new List<Comment>();
        _date = DateTime.Now;
    }

    public Comment(string user, string commenter, string text)
    {
        _reply = true;
        _user = user;
        _text = text;
        _replies = new List<Comment>();
        _date = DateTime.Now;
    }
    public string GetUserName()
    {
        return _user;
    }
    public string GetText()
    {
        return _text;
    }
    public DateTime GetDate()
    {
        return _date;
    }
    public void EditComment(string text)
    {
        _text = text;
        _date = DateTime.Now;
    }
    public void AddReply(string user, string text)
    {
        _replies.Add(new Comment(user, text));
    }
    public List<Comment> GetReplies()
    {
        return _replies;
    }
    public int GetReplyNum()
    {
        return _replies.Count;
    }
    public void PrintReplies()
    {
        Console.WriteLine($"\tReplies:");    
        foreach (var comment in _replies)
        {
            Console.Write("\t\t");
            Console.WriteLine(comment);
        }
    }
    public override string ToString()
    {
        return $"{_user} ({_date}): {_text}";
    }
}