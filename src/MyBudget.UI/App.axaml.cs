using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBudget.Application;
using MyBudget.Data;
using MyBudget.UI.ViewModels;
using MyBudget.UI.Views;
using System;
using System.Linq;

namespace MyBudget.UI;

public class App : Avalonia.Application
{
    public static IServiceProvider Services { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        BindingPlugins.DataValidators
            .OfType<DataAnnotationsValidationPlugin>()
            .ToList()
            .ForEach(v => BindingPlugins.DataValidators.Remove(v));

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            ConfigureServices();
            Services.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            desktop.MainWindow = new MainWindow();
            desktop.MainWindow.DataContext = new MainWindowViewModel(desktop.MainWindow);
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

    public static BudgetApplication GetBudgetApp()
    {
        return Services.GetRequiredService<BudgetApplication>();
    }
}