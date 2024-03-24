using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.UI.Messages;

namespace MyBudget.UI.ViewModels
{
    public partial class NavigationViewViewModel : ViewModelBase
    {
        public NavigationViewViewModel()
        {
            Messenger.RegisterAll(this);
        }

        [RelayCommand]
        public void LoadHomeView() => Messenger.Send(new LoadHomeView());

        [RelayCommand]
        public void LoadExpenseEditorView() => Messenger.Send(new LoadExpenseEditorView());
    }
}
