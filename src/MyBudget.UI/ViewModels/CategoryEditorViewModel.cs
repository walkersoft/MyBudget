using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyBudget.Application;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public partial class CategoryEditorViewModel : ViewModelBase
    {
        private readonly BudgetApplication app = App.GetBudgetApp();

        [ObservableProperty]
        private string categoryName = string.Empty;

        [RelayCommand]
        private async Task SaveCategory()
        {
            await app.CreateCategoryAsync(CategoryName);
        }
    }
}
