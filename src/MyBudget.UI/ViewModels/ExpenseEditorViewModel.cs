using MyBudget.Application;
using MyBudget.Application.Entities;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace MyBudget.UI.ViewModels
{
	public class ExpenseEditorViewModel : ViewModelBase
	{
        private readonly BudgetApplication app;
		public ICommand SaveExpenseCommand { get; }

		private string expenseSource;
		public string ExpenseSource
		{
			get => expenseSource;
			set => this.RaiseAndSetIfChanged(ref expenseSource, value);
		}

		private decimal amount;
		public string Amount
		{
			get => amount.ToString();
			set => this.RaiseAndSetIfChanged(ref amount, decimal.Parse(value));
		}

		private DateTime? effectiveDate;
		public DateTime? EffectiveDate
		{
			get => effectiveDate;
			set => this.RaiseAndSetIfChanged(ref effectiveDate, value);
		}

        private DateTime? expirationDate;
        public DateTime? ExpirationDate
        {
            get => expirationDate;
            set => this.RaiseAndSetIfChanged(ref expirationDate, value);
        }

        public ExpenseEditorViewModel()
        {
            app = App.GetBudgetApp();
			SaveExpenseCommand = ReactiveCommand.CreateFromTask(async () =>
			{
				var expense = await app.CreateExpenseAsync(
					ExpenseSource,
					DateOnly.FromDateTime(EffectiveDate.Value),
					ExpirationDate.HasValue ? DateOnly.FromDateTime(ExpirationDate.Value) : null
				);
				Debug.WriteLine(expense.Id);
			});
        }
    }
}