using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using vnedrenie2Lab.Models;
using Task = vnedrenie2Lab.Models.Task;
using TaskStatus = vnedrenie2Lab.Models.TaskStatus;

namespace vnedrenie2Lab.ViewModel;

public class TasksVM: INotifyPropertyChanged
{
    private User _user;
    private List<Task> _tasksAll = new List<Task>();
    private List<Task> _tasksAllByUser = new List<Task>();
    private List<Task> _tasksBurnByUser = new List<Task>();
    private int _taskCount;
    private Task _taskInfo;
    private Project _project;
    private User _manager;

    public List<Task> TasksAll
    {
        get => _tasksAll;
        set
        {
            if (Equals(value, _tasksAll)) return;
            _tasksAll = value;
            OnPropertyChanged();
        }
    }

    public List<Task> TasksAllByUser
    {
        get => _tasksAllByUser;
        set
        {
            if (Equals(value, _tasksAllByUser)) return;
            _tasksAllByUser = value;
            OnPropertyChanged();
        }
    }

    public List<Task> TasksBURNByUser
    {
        get => _tasksBurnByUser;
        set
        {
            if (Equals(value, _tasksBurnByUser)) return;
            _tasksBurnByUser = value;
            OnPropertyChanged();
        }
    }

    public DB Db { get; set; }

    public int TaskCount
    {
        get => _taskCount;
        set
        {
            if (value == _taskCount) return;
            _taskCount = value;
            OnPropertyChanged();
        }
    }

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

    public Task SelectedTask
    {
        get => _taskInfo;
        set
        {
            if (Equals(value, _taskInfo)) return;
            _taskInfo = value;
            OnPropertyChanged();
        }
    }

    public Project ProjHere
    {
        get => _project;
        set
        {
            if (Equals(value, _project)) return;
            _project = value;
            OnPropertyChanged();
        }
    }

    public User Manager
    {
        get => _manager;
        set
        {
            if (Equals(value, _manager)) return;
            _manager = value;
            OnPropertyChanged();
        }
    }


    public TasksVM(User user)
    {
        Db = DB.GetDB();
        User = user;

        TasksAll = Db.Tasks.ToList();
        TasksAllByUser = Db.Tasks.Where(x => x.UserId == user.Id).ToList();
        TasksBURNByUser = Db.Tasks.Where(x => x.UserId == user.Id && x.Status == TaskStatus.Горит).ToList();
        
        TaskCount =  TasksAllByUser.Count;
        
        ProjHere = Db.Projects
            .FirstOrDefault(p => p.Users.Any(u => u.Id == user.Id));
        
        Manager = Db.Users.Where(u => u.Id == ProjHere.ManagerId).First();
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