using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Koffee.Models;
using Koffee.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace Koffee.ViewModels;

public class AuthorizationVM : ViewModelBase
{
    private string _password;
    private string _login;
    public static User AuthUser { get; set; }

    private User _user;
    public User User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }
    public string Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public ReactiveCommand<Window, Unit> ButtonEnter { get; }

    public AuthorizationVM()
    {
        ButtonEnter = ReactiveCommand.Create<Window>(OpenWindowImpl);
    }
    private void OpenWindowImpl(Window obj)
    {
        User user = null;
        user = Helper.GetContext().Users.SingleOrDefault(x => x.Login == Login & x.Password == Password);
        
        if (user != null)
        {
            SingIn(user, obj);
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка", "Логин или пароль неверны!",
                ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
        }
    }

    public void OpenWindowImp(string Login, string Password)
    {
        User user = null;
        user = Helper.GetContext().Users.SingleOrDefault(x => x.Login == Login & x.Password == Password);
    }
    private void SingIn(User user, Window obj)
    {
        AuthUser = user;
        if (user.Post == "Продавец")
        {
            SellerWindow homePage = new SellerWindow();
            homePage.Show();
            obj.Close();
        }

        else if(user.Post == "Администратор")
        {
            AdminWindow homePage = new AdminWindow();
            homePage.Show();
            obj.Close();
        }
    }
}