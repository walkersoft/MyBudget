using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace MyBudget.UI.ViewModels;

public class ViewModelBase : ObservableValidator
{
    protected static WeakReferenceMessenger Messenger => WeakReferenceMessenger.Default;
    public bool ValidationEnabled { get; set; }
    protected bool CanExecute => !ValidationEnabled || (ValidationEnabled && !HasErrors);
    
    protected void ResetValidation()
    {
        ValidationEnabled = false;
        ClearErrors();
    }
}
