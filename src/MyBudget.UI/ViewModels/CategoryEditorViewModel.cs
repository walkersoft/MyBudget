using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyBudget.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public partial class CategoryEditorViewModel : ObservableObject
    {
        private readonly BudgetApplication app = App.GetBudgetApp();

        [ObservableProperty]
        private string categoryName = string.Empty;
    }
}
