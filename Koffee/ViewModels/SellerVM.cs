using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using Koffee.Models;
using Koffee.Views;
using ReactiveUI;
using System.Linq;
using AvaloniaEdit.Utils;
using Microsoft.EntityFrameworkCore;

namespace Koffee.ViewModels;

public class SellerVM : ViewModelBase
{
    private ObservableCollection<Dish> _dish;
    private ObservableCollection<Order> _order;
    private Order _orders = new Order();
    private static ObservableCollection<Dish> _dishList = new ObservableCollection<Dish>();
    private ObservableCollection<DishList> _selectedDishs = new ObservableCollection<DishList>();
    private ObservableCollection<Dish> _dishInPreorder = new ObservableCollection<Dish>();
    public static ObservableCollection<Dish> Dishes => _dishList;
    private ObservableCollection<Dish> _ordering = new ObservableCollection<Dish>();

    private static User AuthUserNow { get; set; }

    public Order Orders
    {
        get => _orders;
        set => this.RaiseAndSetIfChanged(ref _orders, value);
    }
    
    public ObservableCollection<DishList> SelectedDishs
    {
        get => _selectedDishs;
        set => this.RaiseAndSetIfChanged(ref _selectedDishs, value);
    }
    public ObservableCollection<Dish> Dish
    {
        get => _dish;
        set => this.RaiseAndSetIfChanged(ref _dish, value);
    }

    public ObservableCollection<Order> Order
    {
        get => _order;
        set => this.RaiseAndSetIfChanged(ref _order, value);
    }

    public ObservableCollection<Dish> DishLists
    {
        get => _dishList;
        set => this.RaiseAndSetIfChanged(ref _dishList, value);
    }

    public ObservableCollection<Dish> DishInPreorder
    {
        get => _dishInPreorder;
        set => this.RaiseAndSetIfChanged(ref _dishInPreorder, value);
    }

    public ReactiveCommand<Window, Unit> ButtonProfile { get; }
    public ReactiveCommand<Window, Unit> SubmitOrder { get; }
    public ReactiveCommand<Window, Unit> ExitProfile { get; }
    

    public SellerVM()
    {
        AuthUserNow = AuthorizationVM.AuthUser;
        ButtonProfile = ReactiveCommand.Create<Window>(OpenProfileWindow);
        ExitProfile = ReactiveCommand.Create<Window>(ExitProfileImpl);
        SubmitOrder = ReactiveCommand.Create<Window>(SubmitOrderImpl);
        Dish = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
        Order = new ObservableCollection<Order>(Helper.GetContext().Orders.Include(x => x.DishLists).ThenInclude(x => x.IdDishNavigation).ToList());

    }
    private void SubmitOrderImpl(Window obj)
    {
        CreateOrder(obj);
    }
    private void OpenProfileWindow(Window obj)
    {
        ProfileWindow profile_window = new ProfileWindow();
        profile_window.Show();
        obj.Close();
    }
    
    private void ExitProfileImpl(Window obj)
    {
        AuthUserNow = null;
        AuthorizationWindow aw = new AuthorizationWindow();
        aw.Show();
        obj.Close();
    }

    private void CreateOrder(Window obj)
    {
        Order order = new Order();
        order.Date = DateTime.Now;
        order.IdUser = AuthUserNow.IdUser;
        order.DishLists = _dishList.Select(x => new DishList() { IdDish = x.IdDish, Count = x.Count}).ToList();
        order.Price = _dishList.Sum(x => x.Price);
        Helper.GetContext().Orders.Add(order);
        Helper.GetContext().Orders.UpdateRange();
        Helper.GetContext().SaveChanges();
        _dishList.Clear();
        SellerWindow homePage = new SellerWindow();
        homePage.Show();
        obj.Close();
    }

    private void DishChecking()
    {
        ProfileWindow profile_window = new ProfileWindow();
        profile_window.Show();
    }

    public void AddDishPreorderImpl(Dish dish)
    {
        var edentity = _dishList.SingleOrDefault(x => x.Name == dish.Name);
        if (edentity == null)
        {
            _dishList.Add(new Dish
            {
                IdDish = dish.IdDish,
                Name = dish.Name,
                Price = dish.Price * dish.Count,
                Count = dish.Count
            });
        }
        else
        {
            _dishList.Remove(edentity);
            _dishList.Add(new Dish
            {
                IdDish = dish.IdDish,
                Name = dish.Name,
                Price = dish.Price * dish.Count,
                Count = dish.Count
            });
        }
    }

    public void RemoveDishPreorderImpl(Dish dish)
    {
        _dishList.Remove(dish);
    }
}