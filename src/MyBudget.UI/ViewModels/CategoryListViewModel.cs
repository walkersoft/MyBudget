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
                Messenger.Send(new CategoriesChanged());
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

        async void IRecipient<CategoriesChanged>.Receive(CategoriesChanged message) => await LoadCategoriesAsync();

        async void IRecipient<ExpensesChanged>.Receive(ExpensesChanged message) => await LoadCategoriesAsync();
    }
}
