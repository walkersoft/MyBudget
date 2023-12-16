using MyBudget.Application.Entities;

namespace MyBudget.Application.Common
{
    public interface IAppDataContext
    {
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
    }
}
