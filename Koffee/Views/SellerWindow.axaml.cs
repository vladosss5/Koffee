using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Koffee.ViewModels;
using Koffee.Models;

namespace Koffee.Views;

public partial class SellerWindow : Window
{
    public SellerWindow()
    {
        DataContext = new SellerVM();
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public void AddDishPreorder(Dish dish)
    {
        (DataContext as SellerVM).AddDishPreorderImpl(dish);
    }
    public void RemoveDishPreorder(Dish dish)
    {
        (DataContext as SellerVM).RemoveDishPreorderImpl(dish);
    }
}