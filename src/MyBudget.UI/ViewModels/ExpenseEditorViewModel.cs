using CommunityToolkit.Mvvm.ComponentModel;
using MyBudget.Application;
using MyBudget.Application.Entities;
using MyBudget.UI.Messages;
using ReactiveUI;
using System;
using System.Reactive.Concurrency;
using System.Windows.Input;

namespace MyBudget.UI.ViewModels
{
	public partial class ExpenseEditorViewModel : ObservableObject
	{
        private readonly BudgetApplication app = App.GetBudgetApp();

		public ICommand SaveExpenseCommand { get; }

        [ObservableProperty]
        private int selectedExpenseType;
        [ObservableProperty]
        private string expenseSource = string.Empty;
        [ObservableProperty]
        private string amount = string.Empty;
        [ObservableProperty]
        private DateTime? effectiveDate;
        [ObservableProperty]
        private DateTime? expirationDate;

        public ExpenseEditorViewModel()
        {
            SaveExpenseCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var parsedAmount = decimal.TryParse(Amount, out decimal parsedResult);
                var expense = await app.CreateExpenseAsync(
                    GetExpenseType(),
                    ExpenseSource,
                    DateOnly.FromDateTime(EffectiveDate.Value),
                    ExpirationDate.HasValue ? DateOnly.FromDateTime(ExpirationDate.Value) : null,
                    parsedAmount ? parsedResult : null
                );

                if (expense.Id != Guid.Empty)
                {
                    RxApp.MainThreadScheduler.Schedule(() => MessageBus.Current.SendMessage(new ExpensesChanged()));
                }
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