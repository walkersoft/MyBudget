using MediatR;
using MyBudget.Application.Entities;
using MyBudget.Application.Features.Expenses;

namespace MyBudget.Application
{
    public class BudgetApplication(IMediator mediator)
    {

        public Task<Expense> CreateExpenseAsync(string source, DateOnly effectiveDate, DateOnly? expirationDate = null)
        {
            var request = new CreateExpense(
                ExpenseType: ExpenseType.Variable,
                Source: source,
                EffectiveDate: effectiveDate,
                ExpirationDate: expirationDate
            );

            return mediator.Send(request);
        }
    }
}
