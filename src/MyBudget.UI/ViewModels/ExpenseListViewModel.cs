using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.Application;
using MyBudget.Application.Entities;
using MyBudget.UI.Messages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public readonly record struct ExpenseListing(
        string Source,
        ExpenseType ExpenseType,
        DateOnly EffectiveDate,
        DateOnly? ExpirationDate,
        decimal? Amount,
        string? ExpenseCategory
    );

    public class ExpenseListViewModel : ViewModelBase, IRecipient<ExpensesChanged>
    {
        private readonly BudgetApplication app;

        public ObservableCollection<ExpenseListing> Expenses { get; } = [];

        public ExpenseListViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
                WeakReferenceMessenger.Default.RegisterAll(this);
                LoadExpensesAsync();
            }
        }

        private async Task LoadExpensesAsync()
        {
            var expenses = await app.GetAllExpensesAsync();
            var categories = await app.GetAllCategoriesAsync();

            Expenses.Clear();
            foreach (var expense in expenses)
            {
                Expenses.Add(new ExpenseListing(
                    expense.Source,
                    expense.ExpenseType,
                    expense.EffectiveDate,
                    expense.ExpirationDate,
                    expense.Amount,
                    categories.FirstOrDefault(c => expense.ExpenseCategoryId == c.Id)?.Name
                ));
            }
        }

        void IRecipient<ExpensesChanged>.Receive(ExpensesChanged message)
        {
            LoadExpensesAsync();
        }
    }
}
