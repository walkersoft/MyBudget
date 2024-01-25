﻿using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using MyBudget.Application;
using MyBudget.UI.Messages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public readonly record struct CategoryListing(Guid Id, string Name, int Assignments, bool CanDelete);

    public partial class CategoryListViewModel : ViewModelBase, IRecipient<CategoriesChanged>, IRecipient<ExpensesChanged>
    {
        private readonly BudgetApplication app;

        [ObservableProperty]
        private ObservableCollection<CategoryListing> categories = new();

        public CategoryListViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
                Messenger.RegisterAll(this);
                Messenger.Send(new CategoriesChanged());
            }
        }

        private async Task LoadCategoriesAsync()
        {
            var expenseCategories = await app.GetAllCategoriesAsync();
            var expenses = await app.GetAllExpensesAsync();

            Categories.Clear();
            Categories.AddRange(expenseCategories.Select(category => new CategoryListing()
            {
                Id = category.Id,
                Name = category.Name,
                Assignments = expenses.Count(x => x.ExpenseCategoryId == category.Id),
                CanDelete = !expenses.Any(x => x.ExpenseCategoryId == category.Id)
            }));
        }

        async void IRecipient<CategoriesChanged>.Receive(CategoriesChanged message) => await LoadCategoriesAsync();

        async void IRecipient<ExpensesChanged>.Receive(ExpensesChanged message) => await LoadCategoriesAsync();
    }
}
