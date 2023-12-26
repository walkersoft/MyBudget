using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.Application;
using MyBudget.Application.Entities;
using MyBudget.UI.Messages;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public class ExpenseListViewModel : ObservableObject, IRecipient<ExpensesChanged>
    {
        private readonly BudgetApplication app;

        public ObservableCollection<Expense> Expenses { get; } = new();

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
            var expenses = new ObservableCollection<Expense>(await app.GetAllExpensesAsync());

            Expenses.Clear();
            foreach (var expense in expenses)
            {
                Expenses.Add(expense);
            }
        }

        void IRecipient<ExpensesChanged>.Receive(ExpensesChanged message)
        {
            LoadExpensesAsync();
        }
    }
}
