using MyBudget.Application;
using MyBudget.Application.Common;
using MyBudget.Application.Entities;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.UI.ViewModels
{
    public class ExpenseListViewModel() : ViewModelBase
    {
        private ObservableCollection<Expense> expenses = new();
        public ObservableCollection<Expense> Expenses
        {
            get => expenses;
            set => this.RaiseAndSetIfChanged(ref expenses, value);
        }
    }
}
