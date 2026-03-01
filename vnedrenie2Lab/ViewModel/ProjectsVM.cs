using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using vnedrenie2Lab.Models;

namespace vnedrenie2Lab.ViewModel;

public class ProjectsVM : INotifyPropertyChanged
{
    private User _user;
    private List<Project> _projects = new List<Project>();
    private List<User> _usersOnProject = new List<User>();
    private List<Task> _tasksOnProject = new List<Task>();
    private List<Task> _myTasksOnProject = new List<Task>();
    private Project _selectedProject;
    private int _projCount;
    public DB Db { get; set; }

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

    public List<Project> Projects
    {
        get => _projects;
        set
        {
            if (Equals(value, _projects)) return;
            _projects = value;
            OnPropertyChanged();
        }
    }

    public List<User> UsersOnProject
    {
        get => _usersOnProject;
        set
        {
            if (Equals(value, _usersOnProject)) return;
            _usersOnProject = value;
            OnPropertyChanged();
        }
    }

    public List<Task> TasksOnProject
    {
        get => _tasksOnProject;
        set
        {
            if (Equals(value, _tasksOnProject)) return;
            _tasksOnProject = value;
            OnPropertyChanged();
        }
    }

    public List<Task> MyTasksOnProject
    {
        get => _myTasksOnProject;
        set
        {
            if (Equals(value, _myTasksOnProject)) return;
            _myTasksOnProject = value;
            OnPropertyChanged();
        }
    }

    public Project SelectedProject
    {
        get => _selectedProject;
        set
        {
            if (Equals(value, _selectedProject)) return;
            _selectedProject = value;
            SettColl();
            OnPropertyChanged();
        }
    }

    public int ProjCount
    {
        get => _projCount;
        set
        {
            if (value == _projCount) return;
            _projCount = value;
            OnPropertyChanged();
        }
    }

    public ProjectsVM(User user)
    {
        Db = DB.GetDB();
        User = user;
        
// Найти все проекты, где в списке Users есть пользователь с нужным ID
        Projects = Db.Projects
            .Where(project => project.Users.Any(user => user.Id == User.Id))
            .ToList();
        
        ProjCount = Projects.Count;
        
    }

    public void SettColl()
    {
        UsersOnProject = SelectedProject.Users.ToList();
        TasksOnProject = SelectedProject.Tasks.ToList();;
        MyTasksOnProject = SelectedProject.Tasks.Where(x=> x.UserId == User.Id).ToList();
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