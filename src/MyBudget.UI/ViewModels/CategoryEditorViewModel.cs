using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.Application;
using MyBudget.UI.Messages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public partial class CategoryEditorViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly BudgetApplication app;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCategoryCommand))]
        private string categoryName = string.Empty;

        public CategoryEditorViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
            }
        }

        private bool anAttemptWasMade = false;
        private readonly Dictionary<string, string> errors = [];

        public bool HasErrors => anAttemptWasMade && errors.Count > 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return anAttemptWasMade
                ? errors.GetValueOrDefault(propertyName, null)
                : null;
        }

        private void AddError(string propertyName, string message)
        {
            errors.TryAdd(propertyName, message);
            OnErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        
        private void RemoveError(string propertyName)
        {
            errors.Remove(propertyName);
            OnErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        private void ClearErrors()
        {
            errors.Clear();
        }

        [RelayCommand(CanExecute = nameof(CanSaveCategory))]
        private async Task SaveCategory()
        {
            anAttemptWasMade = true;
            if (CanSaveCategory())
            {
                await app.CreateCategoryAsync(CategoryName);
                Messenger.Send(new CategoriesChanged());
            }
        }

        private bool CanSaveCategory()
        {
            // Validate
            if (anAttemptWasMade)
            {
                ClearErrors();
                if (CategoryName.Length == 0)
                {
                    AddError(nameof(CategoryName), "Category name cannot be empty.");
                }
                else
                {
                    RemoveError(nameof(CategoryName));
                }
            }

            return !HasErrors;
        }
    }
}
