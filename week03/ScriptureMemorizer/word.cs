public class Word
{
    private string _text;
    private string _hiddenText;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _hiddenText = new string('_', text.Length);
        _isHidden = false;
    }

    public string Hide()
    {
        _isHidden = true;
        return _hiddenText;
    }

    public string GetDisplayText()
    {
        return _isHidden ? _hiddenText : _text;
    }

    public string GetText()
    {
        return _text;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }
}
