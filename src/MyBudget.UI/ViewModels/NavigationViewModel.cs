using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.UI.Messages;

namespace MyBudget.UI.ViewModels
{
    public partial class NavigationViewModel : ViewModelBase
    {
        public NavigationViewModel()
        {
            Messenger.RegisterAll(this);
        }

        [RelayCommand]
        public void LoadHomeView() => Messenger.Send(new LoadHomeView());

        [RelayCommand]
        public void LoadExpenseEditorView() => Messenger.Send(new LoadExpenseEditorView());
    }
}
