using WpfSDIAppln.Models;

namespace WpfSDIAppln.ViewModels;

public class EditViewViewModel : ViewModelBase
{
    private readonly DocumentModel _document;
    private string _editContent = "Edit View - Enter texts here";

    public EditViewViewModel(DocumentModel document)
    {
        _document = document;
        _editContent = string.IsNullOrEmpty(_document.Content) 
            ? "Edit View - Enter texts here" 
            : _document.Content;
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
            }
        }
    }

    public void RefreshContent()
    {
        EditContent = string.IsNullOrEmpty(_document.Content) 
            ? "Edit View - Enter texts here" 
            : _document.Content;
    }
}
