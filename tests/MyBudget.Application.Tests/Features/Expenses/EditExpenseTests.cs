using MyBudget.Application.Entities;

namespace MyBudget.Application.Tests.Features.Expenses
{
    public sealed class EditExpenseTests : TestBench
    {
        private readonly Expense expense;

        public EditExpenseTests()
        {
            expense = CreateVariableExpenseAsync().Result;
        }

        [Fact]
        public async Task ExistingExpenseWasEdited_WhenUpdated_WillSucceed()
        {
        }

        [Fact]
        public async Task ExistingExpenseWasEdited_WhenUpdated_HasUpdatedData()
        {

        }

        [Fact]
        public async Task ExpenseDoesNotExist_WhenUpdated_ThrowsException()
        {

        }

        [Fact]
        public async Task ExpenseWithInvalidType_WhenUpdated_ThrowsException()
        {

        }

        [Fact]
        public async Task ExpenseWithSourceRemoved_WhenUpdated_ThrowsException()
        {

        }

        [Fact]
        public async Task ExpenseWithEffectiveDateChangedToDefault_WhenUpdated_ThrowsException()
        {

        }

        [Fact]
        public async Task FixedExpenseWithExpirationDateChangedToDefault_WhenUpdated_ThrowsException()
        {

        }

        [Fact]
        public async Task ExpenseUpdatedToFixedWithoutFutureExpirationDate_WhenUpdated_ThrowsException()
        {

        }

        [Fact]
        public async Task ExpenseUpdatedToStableWithoutAmount_WhenUpdated_ThrowsException()
        {

        }

        [Fact]
        public async Task ExpenseUpdatedToFixedWithoutAmount_WhenUpdated_ThrowsException()
        {

        }

        [Fact]
        public async Task ExpenseUpdatedToStableWithDefaultAmount_WhenUpdated_ThrowsException()
        {

        }

        [Fact]
        public async Task ExpenseUpdatedToFixedWithDefaultAmount_WhenUpdated_ThrowsException()
        {

        }
    }
}
