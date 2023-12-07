using MediatR;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Features.Expenses
{
    internal record CreateExpense(
        ExpenseType ExpenseType,
        string Source,
        DateOnly EffectiveDate,
        DateOnly? ExpirationDate,
        decimal? Amount
    ) : IRequest<Expense>;

    internal class CreateExpenseHandler() : IRequestHandler<CreateExpense, Expense>
    {
        public Task<Expense> Handle(CreateExpense request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
