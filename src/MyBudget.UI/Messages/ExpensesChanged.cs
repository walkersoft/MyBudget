using MediatR;
using MyBudget.Application;
using MyBudget.UI.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyBudget.UI.Messages
{
    internal record ExpensesChanged(ExpenseListViewModel ExpenseListViewModel) : INotification;

    internal class ExpensesChangedHandler(BudgetApplication app) : INotificationHandler<ExpensesChanged>
    {
        public async Task Handle(ExpensesChanged notification, CancellationToken cancellationToken)
        {
            notification.ExpenseListViewModel.Expenses.Clear();
            foreach (var expense in await app.GetAllExpensesAsync())
            {
                notification.ExpenseListViewModel.Expenses.Add(expense);
            }
        }
    }
}
