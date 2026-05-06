using System.Collections.ObjectModel;
using WpfSDIAppln.Models;

namespace WpfSDIAppln.ViewModels;

public class FormViewViewModel : ViewModelBase
{
    private readonly DocumentModel _document;
    private string _selectedItem = string.Empty;

    public FormViewViewModel(DocumentModel document)
    {
        _document = document;

        ComboBoxItems = new ObservableCollection<string>
        {
            "Option 1",
            "Option 2",
            "Option 3"
        };
    }

    public ObservableCollection<string> ComboBoxItems { get; }

    public string SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    public string DisplayText => "Form View";
}
