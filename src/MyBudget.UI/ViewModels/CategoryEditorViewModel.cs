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
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    internal readonly record struct EditingCategory(Guid Id, string CategoryName);

    public partial class CategoryEditorViewModel : ViewModelBase, IRecipient<EditCategory>
    {
        private readonly BudgetApplication app;
        private bool isEditingExisting = false;

        private EditingCategory editingCategory = new();

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
            isEditingExisting = false;
            editingCategory = new();
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task SaveCategory()
        {
            ValidationEnabled = true;
            ValidateAllProperties();
            if (CanExecute())
            {
                if (editingCategory.Id == default)
                {
                    await app.CreateCategoryAsync(CategoryName);
                }
                else
                {
                    var category = new ExpenseCategory()
                    {
                        Id = editingCategory.Id,
                        Name = CategoryName
                    };

                    await app.UpdateCategoryAsync(category);
                }

                Messenger.Send(new CategoriesChanged());
                ResetValidation();
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
                editingCategory = new(category.Id, category.Name);
                CategoryName = category.Name;
                isEditingExisting = true;
                ActivateEditor();
                ResetValidation();
            }
        }
    }
}
