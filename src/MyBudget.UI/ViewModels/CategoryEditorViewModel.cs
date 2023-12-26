using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyBudget.Application;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public partial class CategoryEditorViewModel : ViewModelBase
    {
        private readonly BudgetApplication app;

        [ObservableProperty]
        private string categoryName = string.Empty;

        public CategoryEditorViewModel()
        {
            if (!Design.IsDesignMode)
            {
                app = App.GetBudgetApp();
            }
        }

        [RelayCommand]
        private async Task SaveCategory()
        {
            await app.CreateCategoryAsync(CategoryName);
        }
    }
}
