using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Application.Entities;
using MyBudget.Data;

namespace MyBudget.Application.Tests
{
    abstract public class TestBench : IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ApplicationDbContext context;
        private readonly SqliteConnection connection;

        protected readonly BudgetApplication app;

        protected TestBench()
        {
            // Initialize service container and core services
            var services = new ServiceCollection();
            services.ConfigureBudgetApplication();
            services.ConfigureDataContext();

            // Replace DB context with in-memory provider
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            services.Remove(services.Single(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connection));

            // Build service container
            serviceProvider = services.BuildServiceProvider();

            // Continue with other initialization
            app = serviceProvider.GetRequiredService<BudgetApplication>();
            context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }

        void IDisposable.Dispose()
        {
            connection.Close();
            connection.Dispose();
            context.Database.EnsureDeleted();
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        protected async Task<Expense> CreateVariableExpenseAsync(Guid? categoryId = null)
        {
            return await app.CreateExpenseAsync(
                ExpenseType.Variable,
                "Acme Corp.",
                DateOnly.FromDateTime(DateTime.Today),
                expenseCategoryId: categoryId
            );
        }

        protected async Task<ExpenseCategory> CreateCategoryAsync()
        {
            return await app.CreateCategoryAsync("Test Category");
        }
    }
}
