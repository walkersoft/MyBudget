using FluentAssertions;
using FluentValidation;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Tests.Features.Expenses
{
    public sealed class EditExpenseTests : TestBench
    {
        private Expense expense;
        private Func<Task>? action;

        public EditExpenseTests()
        {
            expense = CreateVariableExpenseAsync().Result;
            action = async () => await app.UpdateExpenseAsync(expense);
        }

        [Fact]
        public async Task ExistingExpenseWasEdited_WhenUpdated_WillSucceed()
        {
            expense.Source = "New Source";

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task ExistingExpenseWasEdited_WhenUpdated_HasUpdatedData()
        {
            var newSource = "New Source";
            expense.Source = newSource;

            await app.UpdateExpenseAsync(expense);
            expense = await app.GetExpenseAsync(expense.Id);

            expense.Source.Should().Be(newSource);
        }

        [Fact]
        public async Task ExpenseDoesNotExist_WhenUpdated_ThrowsException()
        {
            var action = async () => await app.UpdateExpenseAsync(new()
            {
                ExpenseType = ExpenseType.Variable,
                Source = "Source",
                EffectiveDate = DateOnly.FromDateTime(DateTime.Today)
            });

            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task ExpenseWithInvalidType_WhenUpdated_ThrowsException()
        {
            expense.ExpenseType = (ExpenseType)500;

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseWithSourceRemoved_WhenUpdated_ThrowsException()
        {
            expense.Source = string.Empty;

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseWithEffectiveDateChangedToDefault_WhenUpdated_ThrowsException()
        {
            expense.EffectiveDate = default;

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseUpdatedToFixedWithDefaultFutureExpirationDate_WhenUpdated_ThrowsException()
        {
            expense.ExpenseType = ExpenseType.Fixed;
            expense.Amount = 1m;
            expense.ExpirationDate = default;

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseUpdatedToFixedWithoutFutureExpirationDate_WhenUpdated_ThrowsException()
        {
            expense.ExpenseType = ExpenseType.Fixed;
            expense.Amount = 1m;
            expense.ExpirationDate = null;

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseUpdatedToStableWithoutAmount_WhenUpdated_ThrowsException()
        {
            expense.ExpenseType = ExpenseType.Stable;
            expense.Amount = null;

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseUpdatedToFixedWithoutAmount_WhenUpdated_ThrowsException()
        {
            expense.ExpenseType = ExpenseType.Fixed;
            expense.Amount = null;

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseUpdatedToStableWithDefaultAmount_WhenUpdated_ThrowsException()
        {
            expense.ExpenseType = ExpenseType.Stable;
            expense.Amount = default;

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseUpdatedToFixedWithDefaultAmount_WhenUpdated_ThrowsException()
        {
            expense.ExpenseType = ExpenseType.Fixed;
            expense.Amount = default;

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact] 
        public async Task ExpenseUpdatedToFixedWithExpirationBeforeEffective_WhenUpdated_ThrowsExcetion()
        {
            expense.ExpenseType = ExpenseType.Fixed;
            expense.Amount = 1m;
            expense.ExpirationDate = expense.EffectiveDate.AddDays(-1);

            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
