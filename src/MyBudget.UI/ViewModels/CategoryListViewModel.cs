using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using MyBudget.Application;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public record CategoryListing(string Name, int Assignments);

    public partial class CategoryListViewModel : ViewModelBase
    {
        private readonly BudgetApplication app;

        [ObservableProperty]
        private ObservableCollection<CategoryListing> categories = new();

        public CategoryListViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
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
    }
}
