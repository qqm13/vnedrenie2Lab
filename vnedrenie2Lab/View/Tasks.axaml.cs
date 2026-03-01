using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using vnedrenie2Lab.Models;
using vnedrenie2Lab.ViewModel;

namespace vnedrenie2Lab.View;

public partial class Tasks : Window
{
    public Tasks(User user)
    {
        InitializeComponent();
        TasksVM viewModel = new TasksVM(user);
        viewModel.User = user;  // Передаем данные

        this.DataContext = viewModel;  // Устанавливаем DataContext
    }
}