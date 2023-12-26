using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public Interaction<ExpenseEditorViewModel, Unit> ShowExpenseEditor { get; }
    
    public MainWindowViewModel()
    {
        ShowExpenseEditor = new Interaction<ExpenseEditorViewModel, Unit>();
    }

    [RelayCommand]
    private async Task OpenExpenseEditor()
    {
        await ShowExpenseEditor.Handle(new ExpenseEditorViewModel());
    }
}
