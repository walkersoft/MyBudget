using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.UI.Messages;

namespace MyBudget.UI.ViewModels
{
    public partial class NavigationViewModel : ViewModelBase, IRecipient<NavigationPaneToggled>
    {
        public NavigationViewModel()
        {
            Messenger.RegisterAll(this);
        }

        [RelayCommand]
        public void LoadHomeView() => Messenger.Send(new LoadHomeView());

        [RelayCommand]
        public void LoadExpenseEditorView() => Messenger.Send(new LoadExpenseEditorView());

        [ObservableProperty]
        private bool showLabels;
        
        void IRecipient<NavigationPaneToggled>.Receive(NavigationPaneToggled message)
        {
            ShowLabels = message.IsOpen;
        }
    }
}
