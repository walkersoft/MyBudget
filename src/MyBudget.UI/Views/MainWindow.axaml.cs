using Avalonia.ReactiveUI;
using MyBudget.UI.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace MyBudget.UI.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(action => action(ViewModel!.ShowExpenseEditor.RegisterHandler(ShowExpenseEditorAsync)));
    }

    private async Task ShowExpenseEditorAsync(InteractionContext<ExpenseEditorViewModel, Unit> interactionContext)
    {
        var dialog = new ExpenseEditorWindow
        {
            DataContext = interactionContext.Input
        };

        await dialog.ShowDialog(this);
        interactionContext.SetOutput(Unit.Default);
    }
}