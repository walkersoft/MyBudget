using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Features.Cateogry
{
    internal record CreateCategory(string Name) : IRequest<ExpenseCategory>;

    internal class CreateCategoryHandler(IAppDataContext context) : IRequestHandler<CreateCategory, ExpenseCategory>
    {
        public async Task<ExpenseCategory> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var category = new ExpenseCategory()
            {
                Name = request.Name,
            };

            return await context.CreateCategoryAsync(category);
        }
    }
}
