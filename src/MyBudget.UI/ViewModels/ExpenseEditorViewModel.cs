using MyBudget.Application;
using MyBudget.Application.Entities;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace MyBudget.UI.ViewModels
{
	public class ExpenseEditorViewModel : ViewModelBase
	{
        private readonly BudgetApplication app = App.GetBudgetApp();

		public ICommand SaveExpenseCommand { get; }

        private int selectedExpenseType;
        public int SelectedExpenseType
        {
            get => selectedExpenseType;
            set => this.RaiseAndSetIfChanged(ref selectedExpenseType, value);
        }

		private string expenseSource = string.Empty;
		public string ExpenseSource
		{
			get => expenseSource;
			set => this.RaiseAndSetIfChanged(ref expenseSource, value);
		}

		private string amount = string.Empty;
		public string Amount
		{
			get => amount;
			set => this.RaiseAndSetIfChanged(ref amount, value);
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
            SaveExpenseCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var parsedAmount = decimal.TryParse(Amount, out decimal parsedResult);
                await app.CreateExpenseAsync(
                    GetExpenseType(),
                    ExpenseSource,
                    DateOnly.FromDateTime(EffectiveDate.Value),
                    ExpirationDate.HasValue ? DateOnly.FromDateTime(ExpirationDate.Value) : null,
                    parsedAmount ? parsedResult : null
                );
            });
        }

        private ExpenseType GetExpenseType()
        {
            return SelectedExpenseType switch
            {
                0 => ExpenseType.Variable,
                1 => ExpenseType.Stable,
                2 => ExpenseType.Fixed,
                _ => throw new ArgumentException("Invalid expense type selected.")
            };
        }
    }
}