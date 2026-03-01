using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using vnedrenie2Lab.Models;
using vnedrenie2Lab.ViewModel;

namespace vnedrenie2Lab.View;

public partial class Glavnoe : Window
{
    public Glavnoe(User user)
    {
        InitializeComponent();
        
        GlavnoeVM viewModel = new GlavnoeVM(user);
        viewModel.User = user;  // Передаем данные

        this.DataContext = viewModel;  // Устанавливаем DataContext
    }
}