using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Features.Cateogry
{
    internal record GetAllCategories : IRequest<IEnumerable<ExpenseCategory>>;

    internal class GetAllCategoriesHandler(IAppDataContext context) : IRequestHandler<GetAllCategories, IEnumerable<ExpenseCategory>>
    {
        public async Task<IEnumerable<ExpenseCategory>> Handle(GetAllCategories request, CancellationToken cancellationToken)
        {
            return await context.GetAllCategoriesAsync();
        }
    }
}
