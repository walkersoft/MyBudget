using FluentValidation;
using MediatR;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;
using MyBudget.Application.Features.Expenses;

internal readonly record struct UpdateExpense(Expense Expense) : IRequest<Expense>;

internal class UpdateExpenseValidator : AbstractValidator<UpdateExpense>
{
    public UpdateExpenseValidator()
    {
        // Rules for all expenses
        RuleFor(x => x.Expense.ExpenseType).IsInEnum().WithMessage("An unknown expense type was provided.");
        RuleFor(x => x.Expense.Source).NotEmpty().WithMessage("Expense source cannot be empty.");
        RuleFor(x => x.Expense.EffectiveDate).NotEmpty().WithMessage("Effective date is required.");

        // Conditional rules
        RuleFor(x => x.Expense.ExpirationDate)
            .GreaterThan(x => x.Expense.EffectiveDate).When(x => x.Expense.ExpirationDate.HasValue).WithMessage("Expiration date must be after effective date")
            .NotNull().When(x => x.Expense.ExpenseType == ExpenseType.Fixed).WithMessage("Expiration date is required for fixed expenses.");

        RuleFor(x => x.Expense.Amount)
            .Cascade(CascadeMode.Stop)
            .NotNull().When(x => x.Expense.ExpenseType != ExpenseType.Variable).WithMessage("Amount is required for this expense type.")
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}

internal class UpdateExpenseHandler(IAppDataContext context) : IRequestHandler<UpdateExpense, Expense>
{
	public async Task<Expense> Handle(UpdateExpense request, CancellationToken cancellationToken)
	{
        var expense = await context.GetExpenseAsync(request.Expense.Id);

        expense.ExpenseType = request.Expense.ExpenseType;
        expense.Source = request.Expense.Source;
        expense.EffectiveDate = request.Expense.EffectiveDate;
        expense.ExpirationDate = request.Expense.ExpirationDate;
        expense.Amount = request.Expense.Amount;
        expense.ExpenseCategoryId = request.Expense.ExpenseCategoryId;

        return await context.UpdateExpenseAsync(expense);
	}
}
