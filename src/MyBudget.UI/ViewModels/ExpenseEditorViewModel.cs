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
        [Required(ErrorMessage = "An expense type is required.")]
        [NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
        private int selectedExpenseType;
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
        [Required(ErrorMessage = "Expense source is required.")]
        private string expenseSource = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
        [CustomValidation(typeof(ExpenseEditorViewModel), nameof(IsAmountRequired))]
        [CustomValidation(typeof(ExpenseEditorViewModel), nameof(IsAmountValid))]
        private string amount = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
        [Required(ErrorMessage = "Effective date is required for this expense type")]
        [CustomValidation(typeof(ExpenseEditorViewModel), nameof(IsValidEffectiveDate))]
        private DateTime? effectiveDate;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveExpenseCommand))]
        [CustomValidation(typeof(ExpenseEditorViewModel), nameof(IsExpirationDateRequiredAndValid))]
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

        public static ValidationResult IsExpirationDateRequiredAndValid(DateTime? expirationDate, ValidationContext context)
        {
            var instance = (ExpenseEditorViewModel)context.ObjectInstance;

            if (instance.GetExpenseType() == ExpenseType.Fixed && !expirationDate.HasValue)
            {
                return new("Expiration date is required for this expense type.");
            }

            if (instance.EffectiveDate.HasValue && expirationDate.HasValue && expirationDate.Value < instance.EffectiveDate.Value)
            {
                return new("Cannot be before effective date.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult IsAmountRequired(string amount, ValidationContext context)
        {
            var instance = (ExpenseEditorViewModel)context.ObjectInstance;

            if (instance.GetExpenseType() != ExpenseType.Variable && string.IsNullOrEmpty(amount))
            {
                return new("Amount is required for this expense type.");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult IsAmountValid(string amount, ValidationContext context)
        {
            if (!string.IsNullOrEmpty(amount) && !decimal.TryParse(amount, out var _))
            {
                return new("Amount must be a valid dollar amount.");
            }

            return ValidationResult.Success;
        }
    }
}