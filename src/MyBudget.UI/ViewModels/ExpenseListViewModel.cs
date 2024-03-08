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
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public readonly record struct ExpenseListing(
        Guid Id,
        string Source,
        ExpenseType ExpenseType,
        DateOnly EffectiveDate,
        DateOnly? ExpirationDate,
        decimal? Amount,
        string? ExpenseCategory,
        IAsyncRelayCommand<Guid> DeleteExpenseCommand,
        IRelayCommand<Guid> EditExpenseCommand
    );

    public partial class ExpenseListViewModel : ViewModelBase, IRecipient<ExpensesChanged>, IRecipient<CategoriesChanged>
    {
        private readonly BudgetApplication app;

        [ObservableProperty]
        private ObservableCollection<ExpenseListing> expenses = [];

        public ExpenseListViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
                Messenger.RegisterAll(this);
                Messenger.Send(new ExpensesChanged());
            }
        }

        private async Task LoadExpensesAsync()
        {
            var expenses = await app.GetAllExpensesAsync();
            var categories = await app.GetAllCategoriesAsync();

            Expenses.Clear();
            Expenses.AddRange(expenses.Select(expense => new ExpenseListing(
                expense.Id,
                expense.Source,
                expense.ExpenseType,
                expense.EffectiveDate,
                expense.ExpirationDate,
                expense.Amount,
                categories.FirstOrDefault(c => expense.ExpenseCategoryId == c.Id)?.Name,
                new AsyncRelayCommand<Guid>(DeleteExpense),
                new RelayCommand<Guid>(EditExpense)
            )));
        }

        private async Task DeleteExpense(Guid id)
        {
            await app.DeleteExpenseAsync(id);
            Messenger.Send(new ExpensesChanged());
        }

        private void EditExpense(Guid id) => Messenger.Send(new EditExpense(id));

        async void IRecipient<ExpensesChanged>.Receive(ExpensesChanged message) => await LoadExpensesAsync();

        async void IRecipient<CategoriesChanged>.Receive(CategoriesChanged message) => await LoadExpensesAsync();
    }
}
