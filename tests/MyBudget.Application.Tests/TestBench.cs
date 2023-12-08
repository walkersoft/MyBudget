using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Data;

namespace MyBudget.Application.Tests
{
    internal abstract class TestBench : IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ApplicationDbContext context;

        protected readonly BudgetApplication app;

        protected TestBench()
        {
            // Initialize service container and core services
            var services = new ServiceCollection();
            services.ConfigureBudgetApplication();
            services.ConfigureDataContext();

            // Replace DB context with in-memory provider
            services.Remove(services.Single(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=:memory:;Version=3"));
            
            // Build service container
            serviceProvider = services.BuildServiceProvider();

            // Continue with other initialization
            app = serviceProvider.GetRequiredService<BudgetApplication>();
            context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }

        void IDisposable.Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
