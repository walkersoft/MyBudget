using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyBudget.UI.Views;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly Window parentWindow;
    
    public MainWindowViewModel(Window parent)
    {
        parentWindow = parent;
    }

    public MainWindowViewModel()
    {
        
    }

    [ObservableProperty]
    private bool isPaneOpen = true;

    [RelayCommand]
    private void TogglePane() => IsPaneOpen ^= true;

    [RelayCommand]
    private async Task OpenExpenseEditor()
    {
        var dialog = new ExpenseEditorWindow
        {
            DataContext = new ExpenseEditorViewModel()
        };

        await dialog.ShowDialog(parentWindow);
    }
}
