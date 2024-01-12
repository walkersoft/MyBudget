using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.Application;
using MyBudget.Application.Entities;
using MyBudget.UI.Messages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Design;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
	public partial class ExpenseEditorViewModel : ViewModelBase
	{
        private readonly BudgetApplication app;

        [ObservableProperty]
        private bool isEditing = false;

        [ObservableProperty]
        private ExpenseCategory? selectedExpenseCategory;
        
        [ObservableProperty]
        private int selectedExpenseType;
        
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
        [Required(ErrorMessage = "Expense source is required.")]
        private string expenseSource = string.Empty;

        [ObservableProperty]
        private string amount = string.Empty;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
        [Required(ErrorMessage = "Effective date is required for this expense type")]
        [CustomValidation(typeof(ExpenseEditorViewModel), nameof(IsValidEffectiveDate))]
        private DateTime? effectiveDate;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
        private DateTime? expirationDate;

        public ObservableCollection<ExpenseCategory> ExpenseCategories { get; private set; } = [];

        public ExpenseEditorViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
                LoadExpenseCategories();
            }
        }

        private async Task LoadExpenseCategories()
        {
            ExpenseCategories = new ObservableCollection<ExpenseCategory>(await app.GetAllCategoriesAsync());
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task SaveExpense()
        {
            ValidationEnabled = true;
            ValidateAllProperties();

            if (CanExecute())
            {
                var parsedAmount = decimal.TryParse(Amount, out decimal parsedResult);
                var expense = await app.CreateExpenseAsync(
                    GetExpenseType(),
                    ExpenseSource,
                    DateOnly.FromDateTime(EffectiveDate.Value),
                    ExpirationDate.HasValue ? DateOnly.FromDateTime(ExpirationDate.Value) : null,
                    parsedAmount ? parsedResult : null,
                    SelectedExpenseCategory?.Id
                );

                if (expense.Id != Guid.Empty)
                {
                    Messenger.Send(new ExpensesChanged());
                    ResetValidation();
                }
            }
        }

        private ExpenseType GetExpenseType()
        {
            return SelectedExpenseType switch
            {
                0 => ExpenseType.Variable,
                1 => ExpenseType.Stable,
                2 => ExpenseType.Fixed,
                _ => throw new ArgumentException("Invalid expense type selected.")
            };
        }

        [RelayCommand]
        private void ActivateEditor() => IsEditing = true;

        [RelayCommand]
        private void DeactivateEditor()
        {
            IsEditing = false;
        }

        public static ValidationResult IsValidEffectiveDate(DateTime effectiveDate, ValidationContext context)
        {
            var instance = (ExpenseEditorViewModel)context.ObjectInstance;

            if (instance.ExpirationDate.HasValue && effectiveDate > instance.ExpirationDate.Value)
            {
                return new("Cannot be after expiration date.");
            }

            return ValidationResult.Success;
        }
    }
}