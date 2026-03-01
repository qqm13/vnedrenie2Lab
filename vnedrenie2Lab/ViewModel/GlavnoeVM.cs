using System.Windows.Input;
using vnedrenie2Lab.Models;
using vnedrenie2Lab.View;

namespace vnedrenie2Lab.ViewModel;

public class GlavnoeVM
{
    
    public ICommand  ProjectsEnter { get; set; }
    public ICommand  ContractsEnter { get; set; }
    public ICommand  TasksEnter { get; set; }
    public ICommand  NotificationsEnter { get; set; }
    public User User { get; set; }  

    public GlavnoeVM(User user)
    {
        User = user;
        ProjectsEnter = new RelayCommand((() =>
        {
            var mainWindow = new Projects(User);
            mainWindow.Show();
        }));
        ContractsEnter = new RelayCommand((() =>
        {
            var mainWindow = new Contracts(User);
            mainWindow.Show();
        }));
        TasksEnter = new RelayCommand((() =>
        {
            var mainWindow = new Tasks(User);
            mainWindow.Show();
        }));
        NotificationsEnter = new RelayCommand((() =>
        {
            var mainWindow = new Notifications();
            mainWindow.Show();
        }));
    } 
}