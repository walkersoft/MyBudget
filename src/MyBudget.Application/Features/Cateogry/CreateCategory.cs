using FluentValidation;
using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Features.Cateogry
{
    internal readonly record struct CreateCategory(string Name) : IRequest<ExpenseCategory>;

    internal class CreateCategoryValidator : AbstractValidator<CreateCategory>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name must not be empty.");
        }
    }

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
