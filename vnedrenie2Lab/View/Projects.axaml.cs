using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using vnedrenie2Lab.Models;
using vnedrenie2Lab.ViewModel;

namespace vnedrenie2Lab.View;

public partial class Projects : Window
{
    public Projects(User user)
    {
        InitializeComponent();
        ProjectsVM viewModel = new ProjectsVM(user);
        viewModel.User = user;  // Передаем данные

        this.DataContext = viewModel;  // Устанавливаем DataContext
    }
}