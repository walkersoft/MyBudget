using Avalonia.Controls;
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
