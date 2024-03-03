using MediatR;
using MyBudget.Application.Entities;
using MyBudget.Application.Features.Cateogry;
using MyBudget.Application.Features.Expenses;

namespace MyBudget.Application
{
    public class BudgetApplication(IMediator mediator)
    {
        public Task<Expense> CreateExpenseAsync(ExpenseType expenseType, string source, DateOnly effectiveDate, DateOnly? expirationDate = null, decimal? amount = null, Guid? expenseCategoryId = null)
        {
            var request = new CreateExpense(
                ExpenseType: expenseType,
                Source: source,
                EffectiveDate: effectiveDate,
                ExpirationDate: expirationDate,
                Amount: amount,
                ExpenseCategoryId: expenseCategoryId
            );

            return mediator.Send(request);
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await mediator.Send(new GetAllExpenses());
        }

        public async Task<ExpenseCategory> CreateCategoryAsync(string categoryName)
        {
            return await mediator.Send(new CreateCategory(categoryName));
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllCategoriesAsync()
        {
            var categories = await mediator.Send(new GetAllCategories());
            return categories.OrderBy(c => c.Name);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            await mediator.Send(new DeleteCategory(id));
        }

        public async Task DeleteExpenseAsync(Guid id)
        {
            await mediator.Send(new DeleteExpense(id));
        }

        public async Task<ExpenseCategory> UpdateCategoryAsync(ExpenseCategory category)
        {
            return await mediator.Send(new UpdateCategory(category));
        }

        public async Task<ExpenseCategory> GetCategoryAsync(Guid id)
        {
            return await mediator.Send(new GetCategory(id));
        }

        public async Task<Expense> GetExpenseAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
