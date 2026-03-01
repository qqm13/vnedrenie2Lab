using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using vnedrenie2Lab.Models;
using vnedrenie2Lab.ViewModel;

namespace vnedrenie2Lab.View;

public partial class Contracts : Window
{
    public Contracts(User user)
    {
        InitializeComponent();
        
        ContractsVM viewModel = new ContractsVM(user);
        viewModel.User = user;  // Передаем данные

        this.DataContext = viewModel;  // Устанавливаем DataContext
    }
}