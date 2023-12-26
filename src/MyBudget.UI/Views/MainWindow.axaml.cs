using Avalonia.Controls;
using Avalonia.ReactiveUI;
using CommunityToolkit.Mvvm.Input;
using MyBudget.UI.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace MyBudget.UI.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        //this.WhenActivated(action => action(ViewModel!.ShowExpenseEditor.RegisterHandler(ShowExpenseEditorAsync)));
    }

    
    //private async Task ShowExpenseEditor(InteractionContext<ExpenseEditorViewModel, Unit> interactionContext)
    //{
    //    var dialog = new ExpenseEditorWindow
    //    {
    //        DataContext = interactionContext.Input
    //    };

    //    await dialog.ShowDialog(this);
    //    interactionContext.SetOutput(Unit.Default);
    //}
}