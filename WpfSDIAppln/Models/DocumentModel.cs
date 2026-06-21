namespace WpfSDIAppln.Models;

public class DocumentModel
{
    public string Content { get; set; } = string.Empty;
    public bool IsModified { get; set; }
    public string FilePath { get; set; } = string.Empty;
}
