public class Video
{
    public string _title;
    public string _description;
    public int _length;
    public int _likes;
    public int _dislikes;
    public List<Comment> _comments;
    public DateTime _uploadDate;
    public string _uploader;

    public Video(string title, string description, int length, string uploader)
    {
        _title = title;
        _description = description;
        _length = length;
        _likes = 0;
        _dislikes = 0;
        _comments = new List<Comment>();
        _uploadDate = DateTime.Now;
        _uploader = uploader;
    }
    public string GetTitle()
    {
        return _title;
    }
    public string GetDescription()
    {
        return _description;
    }
    public int GetLength()
    {
        return _length;
    }
    public int GetLikes()
    {
        return _likes;
    }
    public int GetDislikes()
    {
        return _dislikes;
    }
    public List<Comment> GetComments()
    {
        return _comments;
    }
    public DateTime GetUploadDate()
    {
        return _uploadDate;
    }
    public string GetUploader()
    {
        return _uploader;
    }
    public void SetTitle(string title)
    {
        _title = title;
    }
    public void SetDescription(string description)
    {
        _description = description;
    }
    public void Like()
    {
        _likes++;
    }
    public void Dislike()
    {
        _dislikes++;
    }
    public void AddComment(string user, string text)
    {
        _comments.Add(new Comment(user, text));
    }
    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }
    public void AddReply(string commenter, string user, string text)
    {
        _comments.Add(new Comment(commenter, user, text));
        Comment comment = GetComment(user);
        if (comment != null)
        {
            comment.AddReply(commenter, text);
        }
    }
    public void RemoveComment(string userName)
    {
        _comments.RemoveAll(comment => comment.GetUserName() == userName);
    }
    public void EditComment(string userName, string newText)
    {
        foreach (var comment in _comments)
        {
            if (comment.GetUserName() == userName)
            {
                comment.EditComment(newText);
                break;
            }
        }
    }
    public Comment GetComment(string user)
    {
        foreach (Comment comment in _comments)
        {
            if (comment.GetUserName() == user)
            {
                return comment;
            }
        }
        Console.WriteLine("Comment Not Found");
        return null;
    }
    public int CommentCount()
    {
        return _comments.Count;
    }
    public void PrintVideoInfo()
    {
        Console.WriteLine($"Title: {_title}");
        Console.WriteLine($"Description: {_description}");
        Console.WriteLine($"Likes: {_likes}");
        Console.WriteLine($"Dislikes: {_dislikes}");
        Console.WriteLine($"Upload Date: {_uploadDate}");
        Console.WriteLine($"Uploader: {_uploader}");
        Console.WriteLine($"Length: {_length}s {(_length < 60 ? $"" : $"({Math.Round((double)_length / 60)}min {_length % 60}sec)")}");
        Console.WriteLine($"{CommentCount()} Comments:");
        foreach (Comment comment in _comments)
        {
            if (comment._reply)
            {
                continue;
            }
            Console.WriteLine($"\t{comment}");
            int reply_num = comment.GetReplyNum();
            if (reply_num > 0)
            {
                comment.PrintReplies();
            }
        }
    }
    public void PrintComments()
    {
        Console.WriteLine("Comments:");
        foreach (var comment in _comments)
        {
            Console.WriteLine(comment);
            comment.PrintReplies();
        }
    }
}