public class Comment
{
    public string User;
    public string Text;
    public List<Comment> Replies;
    public DateTime Date;
    public Boolean Reply;

    public Comment(string user, string text)
    {
        User = user;
        Text = text;
        Replies = new List<Comment>();
        Date = DateTime.Now;
    }

    public Comment(string user, string commenter, string text)
    {
        Reply = true;
        User = user;
        Text = text;
        Replies = new List<Comment>();
        Date = DateTime.Now;
    }
    public Boolean IsReply()
    {
        return Reply;
    }
    public string GetUserName()
    {
        return User;
    }
    public string GetText()
    {
        return Text;
    }
    public DateTime GetDate()
    {
        return Date;
    }
    public void EditComment(string text)
    {
        Text = text;
        Date = DateTime.Now;
    }
    public void AddReply(string user, string text)
    {
        Replies.Add(new Comment(user, text));
    }
    public List<Comment> GetReplies()
    {
        return Replies;
    }
    public int GetReplyNum()
    {
        return Replies.Count;
    }
    public void PrintReplies()
    {
        Console.WriteLine($"\tReplies:");    
        foreach (var comment in Replies)
        {
            Console.Write("\t\t");
            Console.WriteLine(comment);
        }
    }
    public override string ToString()
    {
        return $"{User} ({Date}): {Text}";
    }
}