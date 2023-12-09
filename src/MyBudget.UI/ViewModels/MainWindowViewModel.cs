using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
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
            var editor = new ExpenseEditorViewModel();
            await ShowExpenseEditor.Handle(editor);
            Debug.WriteLine("This happened after the window.");
        });
    }
}
