using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Koffee.ViewModels;

namespace Koffee.Views;

public partial class ChangePasswordWindow : Window
{
    public ChangePasswordWindow()
    {
        DataContext = new ChangePasswordVM();
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