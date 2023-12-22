using MyBudget.Application;
using MyBudget.Application.Entities;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public class ExpenseListViewModel : ViewModelBase
    {
        //private readonly BudgetApplication app = App.GetBudgetApp();

        public ObservableCollection<Expense> Expenses { get; }

        public ExpenseListViewModel()
        {
            Expenses = new ObservableCollection<Expense>
            {
                new Expense()
                {
                    Source = "Verizon",
                    ExpenseType = ExpenseType.Stable,
                    Amount = 200m,
                    EffectiveDate = DateOnly.FromDateTime(DateTime.Now),
                }
            };

            LoadExpensesAsync();
        }

        private async Task LoadExpensesAsync()
        {
            //var expenses = new ObservableCollection<Expense>(await app.GetAllExpensesAsync());
            //Expenses.Clear();
            //foreach (var expense in expenses)
            //{
            //    Expenses.Add(expense);
            //}
        }
    }
}
