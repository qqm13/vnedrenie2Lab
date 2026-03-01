using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using vnedrenie2Lab.View;
using System.Windows.Input;
using vnedrenie2Lab.Models;


namespace vnedrenie2Lab.ViewModel;


public class MainVM : INotifyPropertyChanged
{
    private string _email;
    private string _password;
    public ICommand  EnterCommand { get; set; }
    public DB Db { get; set; }

    public string Email
    {
        get => _email;
        set
        {
            if (value == _email) return;
            _email = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (value == _password) return;
            _password = value;
            OnPropertyChanged();
        }
    }

    public MainVM()
    {
        EnterCommand = new RelayCommand((() =>
        {
            Db = DB.GetDB();
            foreach (var Users in Db.Users)
            {
                if (Users.Email == Email && Users.PasswordHash == Password)
                {
                    var mainWindow = new Glavnoe(Users);
                    mainWindow.Show();
                    //abs@gmail.com
                    //P@ssw0rd
                }
            }
        }));
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}