using System.Reactive;
using Avalonia.Controls;
using Koffee.Models;
using Koffee.Views;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace Koffee.ViewModels;

public class ProfileVM : ViewModelBase 
{
    private static User AuthUserNow { get; set; }
    private User _user;
    
    public ReactiveCommand<Window, Unit> GoBack { get; }
    public ReactiveCommand<Window, Unit> OpenWindowChangePassword { get; }
    public ProfileVM()
    {
        AuthUserNow = AuthorizationVM.AuthUser;
        GoBack = ReactiveCommand.Create<Window>(GoBackImpl);
        OpenWindowChangePassword = ReactiveCommand.Create<Window>(OpenWindowChangePasswordImpl);
    }

    private void OpenWindowChangePasswordImpl(Window obj)
    {
        ChangePasswordWindow changePass = new ChangePasswordWindow();
        changePass.Show();
    }

    private void GoBackImpl(Window obj)
    {
        if (AuthUserNow.Post == "Продавец")
        {
            SellerWindow sellerWindow = new SellerWindow();
            sellerWindow.Show();
            obj.Close();
        }
        else
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            obj.Close();
        }
    }
}