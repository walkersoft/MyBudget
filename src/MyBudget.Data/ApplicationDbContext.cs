using Microsoft.EntityFrameworkCore;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;
using System.Reflection;

namespace MyBudget.Data
{
    public class ApplicationDbContext : DbContext, IAppDataContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\temp\\BudgetDB.db");
        }

        public async Task<Expense> CreateExpenseAsync(Expense expense)
        {
            Set<Expense>().Add(expense);
            await SaveChangesAsync();

            return expense;
        }
    }
}
