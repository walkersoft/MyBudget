using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Features.Expenses
{
    internal record CreateExpense(
        ExpenseType ExpenseType,
        string Source,
        DateOnly EffectiveDate,
        DateOnly? ExpirationDate = null,
        decimal? Amount = null
    ) : IRequest<Expense>;

    internal class CreateExpenseHandler(IAppDataContext context) : IRequestHandler<CreateExpense, Expense>
    {
        public async Task<Expense> Handle(CreateExpense request, CancellationToken cancellationToken)
        {
            var expense = new Expense()
            {
                ExpenseType = request.ExpenseType,
                Source = request.Source,
                EffectiveDate = request.EffectiveDate,
                ExpirationDate = request.ExpirationDate,
                Amount = request.Amount
            };

            return await context.CreateExpenseAsync(expense);
        }
    }
}
