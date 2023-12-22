using Avalonia.Controls;
using MyBudget.Application;
using MyBudget.Application.Entities;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public class ExpenseListViewModel : ViewModelBase
    {
        private readonly BudgetApplication app;

        public ObservableCollection<Expense> Expenses { get; } = new();

        public ExpenseListViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
                RxApp.MainThreadScheduler.Schedule(LoadExpensesAsync);
            }
        }

        private async void LoadExpensesAsync()
        {
            var expenses = new ObservableCollection<Expense>(await app.GetAllExpensesAsync());
            Expenses.Clear();
            foreach (var expense in expenses)
            {
                Expenses.Add(expense);
            }
        }
    }
}
