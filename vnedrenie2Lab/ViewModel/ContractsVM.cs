using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using vnedrenie2Lab.Models;

namespace vnedrenie2Lab.ViewModel;

public class ContractsVM : INotifyPropertyChanged
{
    private User _user;

    public User User
    {
        get => _user;
        set
        {
            if (Equals(value, _user)) return;
            _user = value;
            OnPropertyChanged();
        }
    }
    private List<Contract> _contracts;
    private Contract _selectedContract;

    public List<Contract> Contracts
    {
        get => _contracts;
        set
        {
            _contracts = value;
            OnPropertyChanged();
        }
    }

    public Contract SelectedContract
    {
        get => _selectedContract;
        set
        {
            _selectedContract = value;
            OnPropertyChanged();
        }
    }

    public DB Db { get; set; }
    public ContractsVM(User user)
    {
        Db = DB.GetDB();
        User = user;
        
        // Загрузка из базы данных
        var contracts = Db.Contracts
            .Where(c => c.Projects.Any(p => p.Users.Any(u => u.Id == User.Id)))
            .ToList();
    
        Contracts = new List<Contract>(contracts);
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