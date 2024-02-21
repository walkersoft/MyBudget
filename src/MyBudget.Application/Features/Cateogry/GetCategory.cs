using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Features.Cateogry
{
    internal record struct GetCategory(Guid Id) : IRequest<ExpenseCategory>;

    internal sealed class GetCategoryHandler(IAppDataContext context) : IRequestHandler<GetCategory, ExpenseCategory>
    {
        public async Task<ExpenseCategory> Handle(GetCategory request, CancellationToken cancellationToken)
        {
            return await context.GetCategoryAsync(request.Id);
        }
    }
}
