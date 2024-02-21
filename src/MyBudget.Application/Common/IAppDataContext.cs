using MyBudget.Application.Entities;

namespace MyBudget.Application.Common
{
    public interface IAppDataContext
    {
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<ExpenseCategory> CreateCategoryAsync(ExpenseCategory category);
        Task<IEnumerable<ExpenseCategory>> GetAllCategoriesAsync();
        Task<ExpenseCategory> GetCategoryAsync(Guid id);
        Task DeleteCategoryAsync(Guid id);
        Task DeleteExpenseAsync(Guid id);
        Task<ExpenseCategory> UpdateCategoryAsync(ExpenseCategory category);
    }
}
