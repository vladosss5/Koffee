using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Koffee.ViewModels;

namespace Koffee.Views;

public partial class AddEmployeeWindow : Window
{
    public AddEmployeeWindow()
    {
        DataContext = new AddEmployeeVM();
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}