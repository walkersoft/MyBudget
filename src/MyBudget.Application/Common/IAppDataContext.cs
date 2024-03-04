using MyBudget.Application.Entities;

namespace MyBudget.Application.Common
{
    public interface IAppDataContext
    {
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> GetExpenseAsync(Guid id);
        Task<Expense> UpdateExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(Guid id);
        
        Task<ExpenseCategory> CreateCategoryAsync(ExpenseCategory category);
        Task<IEnumerable<ExpenseCategory>> GetAllCategoriesAsync();
        Task<ExpenseCategory> GetCategoryAsync(Guid id);
        Task<ExpenseCategory> UpdateCategoryAsync(ExpenseCategory category);
        Task DeleteCategoryAsync(Guid id);
    }
}
