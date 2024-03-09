using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using MyBudget.Application;
using MyBudget.Application.Entities;
using MyBudget.UI.Messages;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public partial class ExpenseEditorViewModel : ViewModelBase, IRecipient<CategoriesChanged>, IRecipient<EditExpense>, IRecipient<ExpensesChanged>
	{
        private readonly BudgetApplication app;
        private Guid editingExpenseId = default;

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

        [ObservableProperty]
        private ObservableCollection<ExpenseCategory> expenseCategories = [];

        [ObservableProperty]
        private ObservableCollection<string> expenseTypes = ["Variable", "Stable", "Fixed"];

        public ExpenseEditorViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
                Messenger.RegisterAll(this);
                Messenger.Send(new CategoriesChanged());
            }
        }

        private async Task LoadExpenseCategories()
        {
            ExpenseCategories.Clear();
            ExpenseCategories.AddRange(await app.GetAllCategoriesAsync());
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private async Task SaveExpense()
        {
            ValidationEnabled = true;
            ValidateAllProperties();

            if (CanExecute())
            {
                var parsedAmount = decimal.TryParse(Amount, out decimal parsedResult);
                
                if (editingExpenseId == default)
                {
                    await app.CreateExpenseAsync(
                        GetExpenseType(),
                        ExpenseSource,
                        DateOnly.FromDateTime(DateTime.Parse(EffectiveDate.Value.ToString())),
                        ExpirationDate.HasValue ? DateOnly.FromDateTime(DateTime.Parse(ExpirationDate.Value.ToString())) : null,
                        parsedAmount ? Math.Floor(parsedResult * 100) / 100 : null, // truncates decimal to 2 decimal places
                        SelectedExpenseCategory?.Id
                    );
                }
                else
                {
                    var expense = new Expense()
                    {
                        Id = editingExpenseId,
                        ExpenseType = GetExpenseType(),
                        Source = ExpenseSource,
                        Amount = parsedAmount ? Math.Floor(parsedResult * 100) / 100 : null, // truncates decimal to 2 decimal places
                        EffectiveDate = DateOnly.FromDateTime(DateTime.Parse(EffectiveDate.Value.ToString())),
                        ExpirationDate = ExpirationDate.HasValue ? DateOnly.FromDateTime(DateTime.Parse(ExpirationDate.Value.ToString())) : null,
                        ExpenseCategoryId = SelectedExpenseCategory?.Id
                    };

                    await app.UpdateExpenseAsync(expense);
                }

                Messenger.Send(new ExpensesChanged());
                ResetForm();
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
            ResetForm();
            IsEditing = false;
        }

        private void ResetForm()
        {
            SelectedExpenseType = 0;
            ExpenseSource = string.Empty;
            Amount = string.Empty;
            EffectiveDate = default;
            ExpirationDate = default;
            SelectedExpenseCategory = default;
            ResetValidation();
            SaveExpenseCommand.NotifyCanExecuteChanged();
            editingExpenseId = default;
        }

        async void IRecipient<CategoriesChanged>.Receive(CategoriesChanged message) => await LoadExpenseCategories();

        async void IRecipient<EditExpense>.Receive(EditExpense message)
        {
            var expense = await app.GetExpenseAsync(message.Id);
            if (expense != null)
            {
                SelectedExpenseType = (int)expense.ExpenseType - 1;
                ExpenseSource = expense.Source;
                Amount = expense.Amount.ToString() ?? "";
                EffectiveDate = expense.EffectiveDate.ToDateTime(TimeOnly.MinValue);
                ExpirationDate = expense.ExpirationDate != null ? expense.ExpirationDate.Value.ToDateTime(TimeOnly.MinValue) : null;
                SelectedExpenseCategory = ExpenseCategories.SingleOrDefault(x => x.Id == expense.ExpenseCategoryId);

                ActivateEditor();
                ResetValidation();
                SaveExpenseCommand.NotifyCanExecuteChanged();
                editingExpenseId = expense.Id;
            }
        }

        async void IRecipient<ExpensesChanged>.Receive(ExpensesChanged message)
        {
            // Reset the form when an expense that is being edited has been deleted
            if (editingExpenseId != default)
            {
                var expenses = await app.GetAllExpensesAsync();
                if (!expenses.Select(x => x.Id).Contains(editingExpenseId))
                {
                    ResetForm();
                }
            }
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

        public static ValidationResult? IsAmountValid(string amount, ValidationContext context)
        {
            if (!string.IsNullOrEmpty(amount) && !decimal.TryParse(amount, out var _))
            {
                return new("Amount must be a valid dollar amount.");
            }

            return ValidationResult.Success;
        }
    }
}