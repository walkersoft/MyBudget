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
    }
}
