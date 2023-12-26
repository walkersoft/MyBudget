using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyBudget.UI.Views;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    //public Interaction<ExpenseEditorViewModel, Unit> ShowExpenseEditor { get; }
    private readonly Window parentWindow;
    
    public MainWindowViewModel(Window parent)
    {
        parentWindow = parent;
        //ShowExpenseEditor = new Interaction<ExpenseEditorViewModel, Unit>();
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
