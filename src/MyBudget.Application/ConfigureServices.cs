using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MyBudget.Application
{
    public static class ConfigureServices
    {
        public static void ConfigureBudgetApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<BudgetApplication>();
        }
    }
}
