using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace MyBudget.UI.ViewModels;

public class ViewModelBase : ObservableObject
{
    protected static WeakReferenceMessenger Messenger => WeakReferenceMessenger.Default;
}
