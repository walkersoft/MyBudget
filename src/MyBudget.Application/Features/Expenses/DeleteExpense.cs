using MediatR;
using MyBudget.Application.Common;

namespace MyBudget.Application.Features.Expenses
{
    internal readonly record struct DeleteExpense(Guid Id) : IRequest;

    internal class DeleteExpenseHandler(IAppDataContext context) : IRequestHandler<DeleteExpense>
    {
        public async Task Handle(DeleteExpense request, CancellationToken cancellationToken)
        {
            await context.DeleteExpenseAsync(request.Id);
        }
    }
}
