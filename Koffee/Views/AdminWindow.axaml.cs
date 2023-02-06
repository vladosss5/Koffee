using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Koffee.ViewModels;

namespace Koffee.Views;

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        DataContext = new AdminVM();
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