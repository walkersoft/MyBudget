using Avalonia.Controls;
using MediatR;
using MyBudget.Application;
using MyBudget.Application.Entities;
using MyBudget.UI.Messages;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading;
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
                RxApp.MainThreadScheduler.Schedule(async () => await LoadExpensesAsync());
            }

            MessageBus.Current.Listen<ExpensesChanged>()
                .Subscribe(new AnonymousObserver<ExpensesChanged>(async _ => await LoadExpensesAsync()));
        }

        private async Task LoadExpensesAsync()
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
