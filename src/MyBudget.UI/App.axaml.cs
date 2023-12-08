using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Application;
using MyBudget.Data;
using MyBudget.UI.ViewModels;
using MyBudget.UI.Views;
using System;

namespace MyBudget.UI;

public partial class App : Avalonia.Application
{
    public static IServiceProvider Services { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ConfigureServices();

            var app = Services.GetService<BudgetApplication>();
            var expense = app.CreateExpenseAsync().Result;

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();
        services.ConfigureBudgetApplication();
        services.ConfigureDataContext();

        Services = services.BuildServiceProvider();
    }
}