using WpfSDIAppln.Models;

namespace WpfSDIAppln.ViewModels;

public class MainViewViewModel : ViewModelBase
{
    private readonly DocumentModel _document;

    public MainViewViewModel(DocumentModel document)
    {
        _document = document;
    }

    public string DisplayText => "Displaying MainView";
}
