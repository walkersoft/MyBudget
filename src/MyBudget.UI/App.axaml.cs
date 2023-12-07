using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Application;
using MyBudget.Application.Common;
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

        services.AddSingleton<BudgetApplication>();

        Services = services.BuildServiceProvider();
    }
}