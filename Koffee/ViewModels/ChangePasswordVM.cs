using System.Reactive;
using System.Threading;
using Avalonia.Controls;
using Koffee.Models;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace Koffee.ViewModels;

public class ChangePasswordVM : ViewModelBase
{
    private string _firstpassword;
    private string _secondpassword;
    private string _oldpassword;
    
    public static User AuthUser { get; set; }
    
    public string OldPassword
    {
        get => _oldpassword;
        set => this.RaiseAndSetIfChanged(ref _oldpassword, value);
    }
    public string FirstPassword
    {
        get => _firstpassword;
        set => this.RaiseAndSetIfChanged(ref _firstpassword, value);
    }
    public string SecondPassword
    {
        get => _secondpassword;
        set => this.RaiseAndSetIfChanged(ref _secondpassword, value);
    }
    
    public ReactiveCommand<Window, Unit> ChangeButton { get; }
    public ReactiveCommand<Window, Unit> Cancel { get; }

    public ChangePasswordVM()
    {
        ChangeButton = ReactiveCommand.Create<Window>(SubmitChangeImpl);
        Cancel = ReactiveCommand.Create<Window>(CancelImpl);
    }

    private void CancelImpl(Window obj)
    {
        obj.Close();
    }
    
    private void SubmitChangeImpl(Window obj)
    {
        if (_oldpassword == AuthorizationVM.AuthUser.Password)
        {
            if (_firstpassword == _secondpassword)
            {
                AuthorizationVM.AuthUser.Password = _firstpassword;
                Helper.GetContext().Users.Update(AuthorizationVM.AuthUser);
                Helper.GetContext().SaveChanges();
                MessageBoxManager.GetMessageBoxStandardWindow("Успешно","Пароль изменён",  ButtonEnum.Ok, Icon.Success).ShowDialog(obj);
            }
            else
            {
                MessageBoxManager.GetMessageBoxStandardWindow("Ошибка","Пароли не совпадают",  ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
                return;
            }
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandardWindow("Ошибка","Неверный текущий пароль",  ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
            return;
        }
        
    }
}