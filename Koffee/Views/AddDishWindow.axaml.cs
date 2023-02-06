﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Koffee.ViewModels;

namespace Koffee.Views;

public partial class AddDishWindow : Window
{
    public AddDishWindow()
    {
        DataContext = new AddDishVM();
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