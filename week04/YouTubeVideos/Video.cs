public class Video
{
    public string Title;
    public string Description;
    public int Length;
    public int Likes;
    public int Dislikes;
    public List<Comment> Comments;
    public DateTime UploadDate;
    public string Uploader;

    public Video(string title, string description, int length, string uploader)
    {
        Title = title;
        Description = description;
        Length = length;
        Likes = 0;
        Dislikes = 0;
        Comments = new List<Comment>();
        UploadDate = DateTime.Now;
        Uploader = uploader;
    }
    public string GetTitle()
    {
        return Title;
    }
    public string GetDescription()
    {
        return Description;
    }
    public int GetLength()
    {
        return Length;
    }
    public int GetLikes()
    {
        return Likes;
    }
    public int GetDislikes()
    {
        return Dislikes;
    }
    public List<Comment> GetComments()
    {
        return Comments;
    }
    public DateTime GetUploadDate()
    {
        return UploadDate;
    }
    public string GetUploader()
    {
        return Uploader;
    }
    public void SetTitle(string title)
    {
        Title = title;
    }
    public void SetDescription(string description)
    {
        Description = description;
    }
    public void Like()
    {
        Likes++;
    }
    public void Dislike()
    {
        Dislikes++;
    }
    public void AddComment(string user, string text)
    {
        Comments.Add(new Comment(user, text));
    }
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }
    public void AddReply(string commenter, string user, string text)
    {
        Comments.Add(new Comment(commenter, user, text));
        Comment comment = GetComment(user);
        if (comment != null)
        {
            comment.AddReply(commenter, text);
        }
    }
    public void RemoveComment(string userName)
    {
        Comments.RemoveAll(comment => comment.GetUserName() == userName);
    }
    public void EditComment(string userName, string newText)
    {
        foreach (var comment in Comments)
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
        foreach (Comment comment in Comments)
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
        return Comments.Count;
    }
    public void PrintVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Likes: {Likes}");
        Console.WriteLine($"Dislikes: {Dislikes}");
        Console.WriteLine($"Upload Date: {UploadDate}");
        Console.WriteLine($"Uploader: {Uploader}");
        Console.WriteLine($"Length: {Length}s {(Length < 60 ? $"" : $"({Math.Round((double)Length / 60)}min {Length % 60}sec)")}");
        Console.WriteLine($"{CommentCount()} Comments:");
        foreach (Comment comment in Comments)
        {
            if (comment.Reply)
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
        foreach (var comment in Comments)
        {
            Console.WriteLine(comment);
            comment.PrintReplies();
        }
    }
}