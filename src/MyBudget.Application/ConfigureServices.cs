using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Application.Pipeline;
using System.Reflection;

namespace MyBudget.Application
{
    public static class ConfigureServices
    {
        public static void ConfigureBudgetApplication(this IServiceCollection services)
        {
            services.AddSingleton<BudgetApplication>();
            
            services.AddMediatR(config => {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // Manually scan the assembly for validators since internal classes won't be
            // picked up by FluentValidations automatically
            // https://github.com/FluentValidation/FluentValidation/issues/924#issuecomment-432563178
            var types = Assembly.GetExecutingAssembly().GetTypes();
            new AssemblyScanner(types).ForEach(pair => services.AddTransient(pair.InterfaceType, pair.ValidatorType));
        }
    }
}
