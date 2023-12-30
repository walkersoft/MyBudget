using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.Application;
using MyBudget.Application.Entities;
using MyBudget.UI.Messages;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
	public partial class ExpenseEditorViewModel : ViewModelBase
	{
        private readonly BudgetApplication app;

        [ObservableProperty]
        private ExpenseCategory? selectedExpenseCategory;
        [ObservableProperty]
        private int selectedExpenseType;
        [ObservableProperty]
        private string expenseSource = string.Empty;
        [ObservableProperty]
        private string amount = string.Empty;
        [ObservableProperty]
        private DateTime? effectiveDate;
        [ObservableProperty]
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

        [RelayCommand]
        private async Task SaveExpense()
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
                WeakReferenceMessenger.Default.Send(new ExpensesChanged());
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
    }
}