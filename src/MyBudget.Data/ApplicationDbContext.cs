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
    }
}
