using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;

internal readonly record struct GetExpense(Guid Id) : IRequest<Expense>;

internal class GetExpenseHandler(IAppDataContext context) : IRequestHandler<GetExpense, Expense>
{
	public async Task<Expense> Handle(GetExpense request, CancellationToken cancellationToken)
	{
		return await context.GetExpenseAsync(request.Id);
	}
}
