using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace MyBudget.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand OpenExpenseEditorCommand { get; }
    public Interaction<ExpenseEditorViewModel, Unit> ShowExpenseEditor { get; }
    
    public MainWindowViewModel()
    {
        ShowExpenseEditor = new Interaction<ExpenseEditorViewModel, Unit>();

        OpenExpenseEditorCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            await ShowExpenseEditor.Handle(new ExpenseEditorViewModel());
        });
    }
}
