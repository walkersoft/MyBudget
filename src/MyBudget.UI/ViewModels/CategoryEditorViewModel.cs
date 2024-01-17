using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.Application;
using MyBudget.UI.Messages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public partial class CategoryEditorViewModel : ViewModelBase
    {
        private readonly BudgetApplication app;

        [ObservableProperty]
        private bool isEditing = false;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Category name is required")]
        [NotifyCanExecuteChangedFor(nameof(SaveCategoryCommand))]
        private string categoryName = string.Empty;

        public CategoryEditorViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
            }
        }

        private void ResetForm()
        {
            CategoryName = string.Empty;
            ResetValidation();
            SaveCategoryCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task SaveCategory()
        {
            ValidationEnabled = true;
            ValidateAllProperties();
            if (CanExecute())
            {
                await app.CreateCategoryAsync(CategoryName);
                Messenger.Send(new CategoriesChanged());
                ResetValidation();
            }
        }

        [RelayCommand]
        private void ActivateEditor() => IsEditing = true;

        [RelayCommand]
        private void DeactivateEditor()
        {
            ResetForm();
            IsEditing = false;
        }
    }
}
