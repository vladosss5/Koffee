using System.Net;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Koffee.Context;
using Koffee.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;

namespace Koffee.Views;

public partial class AuthorizationWindow : Window
{
    public AuthorizationWindow()
    {
        InitializeComponent();
        MyDbContext MyDbContext = new MyDbContext();
        // MyDbContext.Database.EnsureCreated();
        DataContext = new AuthorizationVM();
        
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}