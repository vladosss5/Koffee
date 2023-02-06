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

public class AddEmployeeVM : ViewModelBase
{
     private User _user = new User();
        private string _password;
        private string _login;
        private string _name;
        private string _surname;
        private string _patronymic;
        private string _post;
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
        public string Surename
        {
            get => _surname;
            set => this.RaiseAndSetIfChanged(ref _surname, value);
        }
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        public string Patronymic
        {
            get => _patronymic;
            set => this.RaiseAndSetIfChanged(ref _patronymic, value);
        }
        public string Post
        {
            get => _post;
            set => this.RaiseAndSetIfChanged(ref _post, value);
        }
        public ReactiveCommand<Window, Unit> AddEmployee { get; }
        

        public AddEmployeeVM()
        {
            AddEmployee = ReactiveCommand.Create<Window>(AddEmployeeImpl);
        }


        public void AddEmployeeImpl(Window obj)
        {
            var user = Helper.GetContext().Users.FirstOrDefault(x=> x.Login == Login);
            if (user == null)
            {
                _user.Login = _login;
                _user.Password = _password;
                _user.Name = _name;
                _user.Surename = _surname;
                _user.Patronymic = _patronymic;
                _user.Post = _post;
                _user.HoursWorked = 0;
            
                Helper.GetContext().Users.Add(_user);
                try
                {
                    Helper.GetContext().SaveChanges();
                    MessageBoxManager.GetMessageBoxStandardWindow("Сотрудник добавлен", "Нанят", ButtonEnum.Ok, Icon.Success).ShowDialog(obj);
                    obj.Close();
                }
                catch (Exception ex)
                {
                    MessageBoxManager.GetMessageBoxStandardWindow("Данные не заполнены", "Ошибка", ButtonEnum.Ok, Icon.Error).ShowDialog(obj);
                    return;
                }
                if (_user.Login != null & _user.Password != null & _user.Name != null & _user.Surename != null & _user.Patronymic != null & _user.Post != null)
                {
                    MessageBoxManager.GetMessageBoxStandardWindow("Пользователь Зарегистрирован", "Зарегистрирован", ButtonEnum.Ok, Icon.Success).ShowDialog(obj);
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