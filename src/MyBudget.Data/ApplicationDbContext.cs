using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;
using System.Reflection;

namespace MyBudget.Data
{
    public class ApplicationDbContext : DbContext, IAppDataContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            Set<Expense>().Add(expense);
            await SaveChangesAsync();

            return expense;
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            return await Set<Expense>().ToListAsync();
        }

        public async Task<ExpenseCategory> CreateCategoryAsync(ExpenseCategory category)
        {
            Set<ExpenseCategory>().Add(category);
            await SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<ExpenseCategory>> GetAllCategoriesAsync()
        {
            return await Set<ExpenseCategory>().ToListAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var categories = Set<ExpenseCategory>();
            var category = await categories.FindAsync(id);

            if (category is null)
            {
                throw new ArgumentException($"Cannot delete category with ID: {id} - The category does not exist");
            }

            if (Set<Expense>().Where(x => x.ExpenseCategoryId == category.Id).Any())
            {
                throw new ArgumentException($"Cannot delete category with ID: {id} - The category is in use");
            }

            categories.Remove(category);
            await SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(Guid id)
        {
            var expenses = Set<Expense>();
            var expense = await expenses.FindAsync(id);
            expenses.Remove(expense);
            await SaveChangesAsync();
        }

        public async Task<ExpenseCategory> GetCategoryAsync(Guid id) => await Set<ExpenseCategory>().SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new ArgumentException($"Unable to locate category with ID: {id}");

        public async Task<ExpenseCategory> UpdateCategoryAsync(ExpenseCategory category)
        {
            Set<ExpenseCategory>().Update(category);
            await SaveChangesAsync();

            return category;
        }

        public async Task<Expense> GetExpenseAsync(Guid id) =>
            await Set<Expense>().SingleOrDefaultAsync(x => x.Id == id) ?? throw new ArgumentException($"Unable to locate category with ID: {id}");

        public async Task<Expense> UpdateExpenseAsync(Expense expense)
        {
            Set<Expense>().Update(expense);
            await SaveChangesAsync();

            return expense;
        }
    }
}
