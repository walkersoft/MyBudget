using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.UI.Messages;
using MyBudget.UI.Views;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, IRecipient<LoadHomeView>, IRecipient<LoadExpenseEditorView>
{
    private readonly Window parentWindow;
    
    public MainWindowViewModel(Window parent)
    {
        parentWindow = parent;
        Messenger.RegisterAll(this);
        ActiveView = new HomeRootView();
    }

    // This is so the Avalonia designer has something to work with
    public MainWindowViewModel()
    {
        ActiveView = new HomeRootView();
    }

    [ObservableProperty]
    private bool isPaneOpen;

    [ObservableProperty]
    private Visual activeView;

    [RelayCommand]
    private void TogglePane()
    {
        IsPaneOpen ^= true;
        Messenger.Send(new NavigationPaneToggled(IsPaneOpen));
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

    void IRecipient<LoadHomeView>.Receive(LoadHomeView message)
    {
        ActiveView = new HomeRootView();
    }

    void IRecipient<LoadExpenseEditorView>.Receive(LoadExpenseEditorView message)
    {
        ActiveView = new ExpenseEditorRootView();
    }
}
