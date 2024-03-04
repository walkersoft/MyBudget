using FluentAssertions;

namespace MyBudget.Application.Tests.Features.Expenses
{
    public sealed class GetExpenseTests : TestBench
    {
        [Fact]
        public async Task GivenExpenseExists_WhenFetched_WillSucceed()
        {
            var expense = await CreateVariableExpenseAsync();

            var action = async () => await app.GetExpenseAsync(expense.Id);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GivenNonExistentExpense_WhenFetched_ThrowsException()
        {
            var action = async () => await app.GetExpenseAsync(Guid.NewGuid());

            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}
