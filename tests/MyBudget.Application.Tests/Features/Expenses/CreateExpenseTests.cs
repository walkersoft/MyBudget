using FluentAssertions;
using FluentValidation;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Tests.Features.Expenses
{
    public sealed class CreateExpenseTests : TestBench
    {
        [Fact]
        public async Task ValidExpense_WhenCreated_HasNewId()
        {
            var expense = await CreateVariableExpenseAsync();

            expense.Should().NotBeNull();
            expense.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ExpenseWithInvalidType_WhenCreated_ThrowsException()
        {
            var action = async () => await app.CreateExpenseAsync(
                (ExpenseType)500,
                "Source",
                DateOnly.FromDateTime(DateTime.Today)
            );

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseWithoutSource_WhenCreated_ThrowsException()
        {
            var action = async () => await app.CreateExpenseAsync(
                ExpenseType.Variable,
                string.Empty,
                DateOnly.FromDateTime(DateTime.Today)
            );

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task ExpenseWithoutEffectiveDate_WhenCreated_ThrowsException()
        {
            var action = async () => await app.CreateExpenseAsync(
                ExpenseType.Variable,
                "Source",
                default
            );

            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
