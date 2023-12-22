using Avalonia.Controls;
using MyBudget.UI.ViewModels;

namespace MyBudget.UI.Views
{
    public partial class ExpenseListView : UserControl
    {
        public ExpenseListView()
        {
            InitializeComponent();
            //DataContext = new ExpenseListViewModel();
        }
    }
}
