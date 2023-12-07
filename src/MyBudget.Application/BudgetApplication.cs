using MediatR;
using MyBudget.Application.Entities;
using MyBudget.Application.Features.Expenses;

namespace MyBudget.Application
{
    public class BudgetApplication(IMediator mediator)
    {

        public Task<Expense> CreateExpenseAsync()
        {
            var request = new CreateExpense(
                ExpenseType: ExpenseType.Stable,
                Source: "The void",
                EffectiveDate: DateOnly.FromDateTime(DateTime.Today),
                ExpirationDate: DateOnly.FromDateTime(DateTime.Today.AddMonths(6)),
                Amount: 100m
            );

            return mediator.Send(request);
        }
    }
}
