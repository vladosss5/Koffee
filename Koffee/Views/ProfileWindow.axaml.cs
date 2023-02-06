using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Koffee.ViewModels;

namespace Koffee.Views;

public partial class ProfileWindow : Window
{
    public ProfileWindow()
    {
        DataContext = new ProfileVM();
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