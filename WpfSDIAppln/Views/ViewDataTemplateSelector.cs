using System.Windows;
using System.Windows.Controls;
using WpfSDIAppln.ViewModels;

namespace WpfSDIAppln.Views;

public class ViewDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate? MainViewTemplate { get; set; }
    public DataTemplate? FormViewTemplate { get; set; }
    public DataTemplate? EditViewTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        return item switch
        {
            MainViewViewModel => MainViewTemplate,
            FormViewViewModel => FormViewTemplate,
            EditViewViewModel => EditViewTemplate,
            _ => base.SelectTemplate(item, container)
        };
    }
}
