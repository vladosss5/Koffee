using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls;
using Koffee.Models;
using Koffee.Views;
using ReactiveUI;
using System.Linq;
using System.IO;
using AvaloniaEdit.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace Koffee.ViewModels;

public class AdminVM : ViewModelBase
{
    private ObservableCollection<Order> _order;
    private Order _orders = new Order();
    private ObservableCollection<User> _user;
    private User _users = new User();
    private static ObservableCollection<Dish> _dishList = new ObservableCollection<Dish>();
    private ObservableCollection<Dish> _dish;
    private static User AuthUserNow { get; set; }
    

    public Order Orders
    {
        get => _orders;
        set => this.RaiseAndSetIfChanged(ref _orders, value);
    }
    
    public ObservableCollection<Dish> DishLists
    {
        get => _dishList;
        set => this.RaiseAndSetIfChanged(ref _dishList, value);
    }
    
    public ObservableCollection<Order> Order
    {
        get => _order;
        set => this.RaiseAndSetIfChanged(ref _order, value);
    }

    public User Users
    {
        get => _users;
        set => this.RaiseAndSetIfChanged(ref _users, value);
    }

    public ObservableCollection<User> User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }
    
    public ObservableCollection<Dish> Dish
    {
        get => _dish;
        set => this.RaiseAndSetIfChanged(ref _dish, value);
    }

    public ReactiveCommand<Window, Unit> ButtonProfile { get; }
    public ReactiveCommand<Window, Unit> OpenWindowChangePassword { get; }
    public ReactiveCommand<Window, Unit> AddEmployee { get; }
    public ReactiveCommand<Window, Unit> CreateReport { get; }
    public ReactiveCommand<Window, Unit> AddDish { get; }
    

    public AdminVM()
    {
        AuthUserNow = AuthorizationVM.AuthUser;
        ButtonProfile = ReactiveCommand.Create<Window>(OpenProfileWindow);
        AddEmployee = ReactiveCommand.Create<Window>(AddEmployeeM);
        CreateReport = ReactiveCommand.Create<Window>(CreateReportImpl);
        AddDish = ReactiveCommand.Create<Window>(AddDishM);
        OpenWindowChangePassword = ReactiveCommand.Create<Window>(OpenWindowChangePasswordImpl);
        Order = new ObservableCollection<Order>(Helper.GetContext().Orders.Include(x => x.DishLists).ToList());
        User = new ObservableCollection<User>(Helper.GetContext().Users.ToList());
        Dish = new ObservableCollection<Dish>(Helper.GetContext().Dishes.ToList());
    }
    
    private void CreateReportImpl(Window obj)
    {
        using (ExcelHelper helper = new ExcelHelper())
        {
            if (helper.Open(filePath: Path.Combine(Environment.CurrentDirectory, @"D:\Документы\Отчёт о заказах.xlsx")))
            {
                int i = 0;
                var allOrders = Order.ToList().OrderBy(p => p.Date).ToList();
                var application = new Excel.Application();
                string[] month = new string[12] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Окрябрь", "Ноябрь", "Декабрь" };
                application.SheetsInNewWorkbook = month.Length;

                Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                
                for (int j = 0; j < month.Length; ++j)
                {
                    int counter = 0;
                    int startRowIndex = 1;
                    
                    Excel.Worksheet worksheet = application.Worksheets.Item[j + 1];
                    worksheet.Name = month[j];

                    worksheet.Cells[1][startRowIndex] = "Номер";
                    worksheet.Cells[2][startRowIndex] = "Блюда";
                    worksheet.Cells[3][startRowIndex] = "Кассир";
                    worksheet.Cells[4][startRowIndex] = "Дата";
                    worksheet.Cells[5][startRowIndex] = "Цена";

                    startRowIndex++;

                    while (allOrders.Count > i)
                    {
                        if (allOrders[i].Date.Month == j + 1)
                        {
                            worksheet.Cells[1][startRowIndex] = allOrders[i].IdOrder;
                            worksheet.Cells[2][startRowIndex] = "Кофе";
                            worksheet.Cells[3][startRowIndex] = allOrders[i].IdUser;
                            worksheet.Cells[4][startRowIndex] = allOrders[i].Date.ToString("yy-MM-dd");
                            worksheet.Cells[5][startRowIndex] = allOrders[i].Price;
                            counter++;
                        }
                        else
                        {
                            break;
                        }
                        i++;
                        startRowIndex++;
                    }
                    
                    Excel.Range sumRange = worksheet.Range[worksheet.Cells[1][startRowIndex],
                        worksheet.Cells[4][startRowIndex]];
                    sumRange.Merge();
                    sumRange.Value = "Итого:";

                    worksheet.Cells[5][startRowIndex].Formula =
                        $"=SUM(E{startRowIndex - counter}:" + $"E{startRowIndex - 1}";
                    // worksheet.Cells[5][startRowIndex].NumberFormat = worksheet.Cells[5][startRowIndex] = "#,###.00";
                    
                    worksheet.Columns.AutoFit();
                    helper.Save();
                }
                application.Visible = true;
            }
        }
    }

    private void AddDishM(Window obj)
    {
        AddDishWindow adm = new AddDishWindow();
        adm.Show();
    }
    private void OpenProfileWindow(Window obj)
    {
        ProfileWindow profile_window = new ProfileWindow();
        profile_window.Show();
        obj.Close();
    }
    
    private void OpenWindowChangePasswordImpl(Window obj)
    {
        ChangePasswordWindow changePass = new ChangePasswordWindow();
        changePass.Show();
    }

    private void AddEmployeeM(Window obj)
    {
        AddEmployeeWindow aem = new AddEmployeeWindow();
        aem.Show();
    }
}