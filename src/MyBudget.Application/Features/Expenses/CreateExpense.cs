using FluentValidation;
using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Features.Expenses
{
    internal readonly record struct CreateExpense(
        ExpenseType ExpenseType,
        string Source,
        DateOnly EffectiveDate,
        DateOnly? ExpirationDate = null,
        decimal? Amount = null,
        Guid? ExpenseCategoryId = null
    ) : IRequest<Expense>;

    internal class CreateExpenseValidator : AbstractValidator<CreateExpense>
    {
        public CreateExpenseValidator()
        {
            // Rules for all expenses
            RuleFor(x => x.ExpenseType).IsInEnum().WithMessage("An unknown expense type was provided.");
            RuleFor(x => x.Source).NotEmpty().WithMessage("Expense source cannot be empty.");
            RuleFor(x => x.EffectiveDate).NotEmpty().WithMessage("Effective date is required.");

            // Conditional rules
            RuleFor(x => x.ExpirationDate)
                .GreaterThan(x => x.EffectiveDate).When(x => x.ExpirationDate.HasValue).WithMessage("Expiration date must be after effective date")
                .NotNull().When(x => x.ExpenseType == ExpenseType.Fixed).WithMessage("Expiration date is required for fixed expenses.");

            RuleFor(x => x.Amount)
                .Cascade(CascadeMode.Stop)
                .NotNull().When(x => x.ExpenseType != ExpenseType.Variable).WithMessage("Amount is required for this expense type.")
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        }
    }

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
                Amount = request.Amount,
                ExpenseCategoryId = request.ExpenseCategoryId,
            };

            return await context.CreateExpenseAsync(expense);
        }
    }
}
