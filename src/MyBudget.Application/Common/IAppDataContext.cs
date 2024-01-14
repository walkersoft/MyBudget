using MyBudget.Application.Entities;

namespace MyBudget.Application.Common
{
    public interface IAppDataContext
    {
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<ExpenseCategory> CreateCategoryAsync(ExpenseCategory category);
        Task<IEnumerable<ExpenseCategory>> GetAllCategoriesAsync();
        Task DeleteCategoryAsync(Guid id);
    }
}
