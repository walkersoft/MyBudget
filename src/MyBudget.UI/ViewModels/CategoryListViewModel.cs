using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using MyBudget.Application;
using MyBudget.UI.Messages;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public readonly record struct CategoryListing(string Name, int Assignments);

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
                LoadCategoriesAsync();
            }
        }

        private async Task LoadCategoriesAsync()
        {
            var expenseCategories = await app.GetAllCategoriesAsync();
            var expenses = await app.GetAllExpensesAsync();

            Categories.Clear();
            foreach (var category in expenseCategories)
            {
                Categories.Add(new CategoryListing(
                    category.Name,
                    expenses.Count(x => x.ExpenseCategoryId == category.Id)
                ));
            }
        }

        void IRecipient<CategoriesChanged>.Receive(CategoriesChanged message)
        {
            LoadCategoriesAsync();
        }

        void IRecipient<ExpensesChanged>.Receive(ExpensesChanged message)
        {
            LoadCategoriesAsync();
        }
    }
}
