using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.Application.Features.Cateogry
{
    internal record struct UpdateCategory(ExpenseCategory Category) : IRequest<ExpenseCategory>;

    internal class UpdateCategoryHandler(IAppDataContext context) : IRequestHandler<UpdateCategory, ExpenseCategory>
    {
        public async Task<ExpenseCategory> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            var category = await context.GetCategoryAsync(request.Category.Id);
            category.Name = request.Category.Name;
            return await context.UpdateCategoryAsync(category);
        }
    }
}
