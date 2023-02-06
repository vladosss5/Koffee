using System;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Koffee.Models;
using Koffee.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace Koffee.ViewModels;

public class AddDishVM : ViewModelBase
{
    private Dish _dish = new Dish();
    private string _name;
    private float _price;

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public float Price
    {
        get => _price;
        set => this.RaiseAndSetIfChanged(ref _price, value);
    }
    
    public ReactiveCommand<Window, Unit> AddDish { get; }

    public AddDishVM()
    {
        AddDish = ReactiveCommand.Create<Window>(AddDishImpl);
    }

    private void AddDishImpl(Window obj)
    {
        var dish = Helper.GetContext().Dishes.FirstOrDefault(x=> x.Name == Name);
        if (dish == null)
        {
            _dish.Name = _name;
            _dish.Price = _price;
            
            Helper.GetContext().Dishes.Add(_dish);
            try
            {
                Helper.GetContext().SaveChanges();
                MessageBoxManager.GetMessageBoxStandardWindow("Блюдо добавлено", "Готово", ButtonEnum.Ok, Icon.Success).ShowDialog(obj);
                obj.Close();
            }
            catch (Exception ex)
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Данные не заполнены", "Ошибка", ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
                return;
            }
            if (_dish.Name != null & _dish.Price != null)
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Блюдо добавлено", "Готово", ButtonEnum.Ok, Icon.Success).ShowDialog(obj);
                AuthorizationWindow authorization = new AuthorizationWindow();
            }
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Данный логин занят", "Ошибка", ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
            return;
        }
    }
}