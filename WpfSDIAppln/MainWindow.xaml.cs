using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfSDIAppln.ViewModels;

namespace WpfSDIAppln;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly DispatcherTimer _keyboardIndicatorTimer;

    public MainWindow()
    {
        InitializeComponent();
        var viewModel = new MainViewModel();
        DataContext = viewModel;

        _keyboardIndicatorTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500)
        };
        _keyboardIndicatorTimer.Tick += (s, e) =>
        {
            viewModel.CapsLockOn = Console.CapsLock;
            viewModel.NumLockOn = Console.NumberLock;
        };
        _keyboardIndicatorTimer.Start();
    }

    protected override void OnClosed(EventArgs e)
    {
        _keyboardIndicatorTimer?.Stop();
        base.OnClosed(e);
    }
}