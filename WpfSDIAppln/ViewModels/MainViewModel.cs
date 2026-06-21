using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfSDIAppln.Models;

namespace WpfSDIAppln.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase? _currentView;
    private string _selectedViewName = "Main View";
    private string _statusBarText = "Ready";
    private bool _capsLockOn;
    private bool _numLockOn;

    public MainViewModel()
    {
        ViewNames = new ObservableCollection<string>
        {
            "Main View",
            "Form View",
            "Edit View"
        };

        Document = new DocumentModel();

        MainView = new MainViewViewModel(Document);
        FormView = new FormViewViewModel(Document);
        EditView = new EditViewViewModel(Document);

        CurrentView = MainView;

        NewDocumentCommand = new RelayCommand(_ => NewDocument());
        OpenDocumentCommand = new RelayCommand(_ => OpenDocument());
        SaveDocumentCommand = new RelayCommand(_ => SaveDocument());
        PrintCommand = new RelayCommand(_ => Print());
        ExitCommand = new RelayCommand(_ => Exit());
        AboutCommand = new RelayCommand(_ => ShowAbout());
    }

    public DocumentModel Document { get; }

    public ObservableCollection<string> ViewNames { get; }

    public MainViewViewModel MainView { get; }
    public FormViewViewModel FormView { get; }
    public EditViewViewModel EditView { get; }

    public ViewModelBase? CurrentView
    {
        get => _currentView;
        set => SetProperty(ref _currentView, value);
    }

    public string SelectedViewName
    {
        get => _selectedViewName;
        set
        {
            if (SetProperty(ref _selectedViewName, value))
            {
                SwitchView(value);
            }
        }
    }

    public string StatusBarText
    {
        get => _statusBarText;
        set => SetProperty(ref _statusBarText, value);
    }

    public bool CapsLockOn
    {
        get => _capsLockOn;
        set => SetProperty(ref _capsLockOn, value);
    }

    public bool NumLockOn
    {
        get => _numLockOn;
        set => SetProperty(ref _numLockOn, value);
    }

    public ICommand NewDocumentCommand { get; }
    public ICommand OpenDocumentCommand { get; }
    public ICommand SaveDocumentCommand { get; }
    public ICommand PrintCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand AboutCommand { get; }

    private void SwitchView(string viewName)
    {
        CurrentView = viewName switch
        {
            "Main View" => MainView,
            "Form View" => FormView,
            "Edit View" => EditView,
            _ => MainView
        };

        StatusBarText = $"Switched to {viewName}";
    }

    private void NewDocument()
    {
        Document.Content = string.Empty;
        Document.IsModified = false;
        Document.FilePath = string.Empty;
        StatusBarText = "New document created";

        if (CurrentView is EditViewViewModel editView)
        {
            editView.RefreshContent();
        }
    }

    private void OpenDocument()
    {
        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        };

        if (dialog.ShowDialog() == true)
        {
            try
            {
                Document.Content = System.IO.File.ReadAllText(dialog.FileName);
                Document.FilePath = dialog.FileName;
                Document.IsModified = false;
                StatusBarText = $"Opened: {dialog.FileName}";

                if (CurrentView is EditViewViewModel editView)
                {
                    editView.RefreshContent();
                }
            }
            catch (Exception ex)
            {
                StatusBarText = $"Error opening file: {ex.Message}";
            }
        }
    }

    private void SaveDocument()
    {
        if (string.IsNullOrEmpty(Document.FilePath))
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                Document.FilePath = dialog.FileName;
            }
            else
            {
                return;
            }
        }

        try
        {
            if (CurrentView is EditViewViewModel editView)
            {
                Document.Content = editView.EditContent;
            }

            System.IO.File.WriteAllText(Document.FilePath, Document.Content);
            Document.IsModified = false;
            StatusBarText = $"Saved: {Document.FilePath}";
        }
        catch (Exception ex)
        {
            StatusBarText = $"Error saving file: {ex.Message}";
        }
    }

    private void Print()
    {
        StatusBarText = "Print functionality would be implemented here";
    }

    private void Exit()
    {
        System.Windows.Application.Current.Shutdown();
    }

    private void ShowAbout()
    {
        System.Windows.MessageBox.Show(
            "WPF SDI Application\nConverted from MFC\n\nCopyright (c) Microsoft Corporation",
            "About WPF SDI Application",
            System.Windows.MessageBoxButton.OK,
            System.Windows.MessageBoxImage.Information);
    }
}
