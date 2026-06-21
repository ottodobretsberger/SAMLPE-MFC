using WpfSDIAppln.Models;

namespace WpfSDIAppln.ViewModels;

public class EditViewViewModel : ViewModelBase
{
    private readonly DocumentModel _document;
    private bool _isUpdating;
    private string _editContent = string.Empty;

    public EditViewViewModel(DocumentModel document)
    {
        _document = document;
        _editContent = string.IsNullOrEmpty(_document.Content)
            ? BuildTemplate()
            : _document.Content;

        UpdateReportCompleteness();
    }

    public string EditContent
    {
        get => _editContent;
        set
        {
            if (SetProperty(ref _editContent, value))
            {
                _document.Content = value;
                _document.IsModified = true;

                if (!_isUpdating)
                {
                    UpdateReportCompleteness();
                }
            }
        }
    }

    public void RefreshContent()
    {
        EditContent = string.IsNullOrEmpty(_document.Content)
            ? BuildTemplate()
            : _document.Content;
    }

    private static string BuildTemplate()
    {
        return "Structured Imaging Report\r\n" +
               "=========================\r\n" +
               "Clinical Context:\r\n" +
               "Technique:\r\n" +
               "Findings:\r\n" +
               "Impression:\r\n" +
               "Recommended Follow-up:\r\n\r\n" +
               "Completeness: 0%\r\n" +
               "Status: Draft";
    }

    private void UpdateReportCompleteness()
    {
        var text = _editContent;
        var tags = new[]
        {
            "Clinical Context:",
            "Technique:",
            "Findings:",
            "Impression:",
            "Recommended Follow-up:"
        };

        var completedSections = 0;
        foreach (var tag in tags)
        {
            var tagIndex = text.IndexOf(tag, StringComparison.Ordinal);
            if (tagIndex < 0)
            {
                continue;
            }

            var valueStart = tagIndex + tag.Length;
            var valueEnd = text.IndexOf('\n', valueStart);
            if (valueEnd < 0)
            {
                valueEnd = text.Length;
            }

            var lineValue = text[valueStart..valueEnd].Trim();
            if (!string.IsNullOrWhiteSpace(lineValue))
            {
                completedSections++;
            }
        }

        var percent = completedSections * 100 / tags.Length;
        var status = percent >= 80 ? "Ready for review" : "Draft";

        var updated = ReplaceLine(text, "Completeness:", $"Completeness: {percent}%");
        updated = ReplaceLine(updated, "Status:", $"Status: {status}");

        if (!string.Equals(updated, _editContent, StringComparison.Ordinal))
        {
            _isUpdating = true;
            _editContent = updated;
            OnPropertyChanged(nameof(EditContent));
            _document.Content = updated;
            _isUpdating = false;
        }
    }

    private static string ReplaceLine(string source, string prefix, string replacement)
    {
        var lineStart = source.IndexOf(prefix, StringComparison.Ordinal);
        if (lineStart < 0)
        {
            return source;
        }

        var lineEnd = source.IndexOf('\n', lineStart);
        if (lineEnd < 0)
        {
            lineEnd = source.Length;
        }

        var before = source[..lineStart];
        var after = source[lineEnd..];
        return before + replacement + after;
    }
}
