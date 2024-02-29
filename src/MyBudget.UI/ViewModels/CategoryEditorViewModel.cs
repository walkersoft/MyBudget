using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.Application;
using MyBudget.Application.Entities;
using MyBudget.UI.Messages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public partial class CategoryEditorViewModel : ViewModelBase, IRecipient<EditCategory>, IRecipient<CategoriesChanged>
    {
        private readonly BudgetApplication app;
        private Guid editingCategoryId = default;

        [ObservableProperty]
        private bool isEditorActive = false;

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
                Messenger.RegisterAll(this);
            }
        }

        private void ResetForm()
        {
            CategoryName = string.Empty;
            ResetValidation();
            SaveCategoryCommand.NotifyCanExecuteChanged();
            editingCategoryId = default;
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task SaveCategory()
        {
            ValidationEnabled = true;
            ValidateAllProperties();
            if (CanExecute())
            {
                if (editingCategoryId == default)
                {
                    await app.CreateCategoryAsync(CategoryName);
                }
                else
                {
                    var category = new ExpenseCategory()
                    {
                        Id = editingCategoryId,
                        Name = CategoryName
                    };

                    await app.UpdateCategoryAsync(category);
                }

                Messenger.Send(new CategoriesChanged());
                ResetForm();
            }
        }

        [RelayCommand]
        private void ActivateEditor() => IsEditorActive = true;

        [RelayCommand]
        private void DeactivateEditor()
        {
            ResetForm();
            IsEditorActive = false;
        }

        async void IRecipient<EditCategory>.Receive(EditCategory message)
        {
            var category = await app.GetCategoryAsync(message.Id);
            if (category != null)
            {
                editingCategoryId = category.Id;
                CategoryName = category.Name;
                ActivateEditor();
                ResetValidation();
            }
        }

        async void IRecipient<CategoriesChanged>.Receive(CategoriesChanged message)
        {
            // Reset the form when a category that is being edited has been deleted
            if (editingCategoryId != default)
            {
                var categories = await app.GetAllCategoriesAsync();
                if (!categories.Select(x => x.Id).Contains(editingCategoryId))
                {
                    ResetForm();
                }
            }
        }
    }
}
