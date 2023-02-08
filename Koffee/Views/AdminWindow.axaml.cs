using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Koffee.Models;
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
    public void RemoveEmployee(User user)
    {
        (DataContext as AdminVM).RemoveEmployeeImpl(user);
    }
    
    // public void RemoveDish(Dish dish)
    // {
    //     (DataContext as AdminVM).RemoveDishImpl(dish);
    // }
}