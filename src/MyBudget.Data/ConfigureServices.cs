using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Application.Common;

namespace MyBudget.Data
{
    public static class ConfigureServices
    {
        public static void ConfigureDataContext(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=C:\\temp\\BudgetDB.db"));
            services.AddSingleton<IAppDataContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        }
    }
}
