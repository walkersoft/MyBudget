using FluentAssertions;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Tests.Features.Expenses
{
    public sealed class CreateExpenseTests : TestBench
    {
        [Fact]
        public async Task ValidExpense_WhenCreated_HasNewId()
        {
            var expense = await app.CreateExpenseAsync(
                ExpenseType.Variable,
                "Acme Corp.",
                DateOnly.FromDateTime(DateTime.Today)
            );

            expense.Should().NotBeNull();
            expense.Id.Should().NotBeEmpty();
        }
    }
}
