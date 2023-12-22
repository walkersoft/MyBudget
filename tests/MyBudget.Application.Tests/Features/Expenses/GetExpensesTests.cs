using FluentAssertions;

namespace MyBudget.Application.Tests.Features.Expenses
{
    public sealed class GetExpensesTests : TestBench
    {
        [Fact]
        public async Task GivenNoExpensesSaved_WhenFetched_ReturnsNoExpenses()
        {
            var results = await app.GetAllExpensesAsync();
            results.Count().Should().Be(0);
        }

        [Fact]
        public async Task GivenExpensesExist_WhenFetched_ReturnsAll()
        {
            await CreateVariableExpenseAsync();

            var results = await app.GetAllExpensesAsync();

            results.Count().Should().Be(1);
        }
    }
}
