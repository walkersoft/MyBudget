using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBudget.Application.Tests.Features.Expenses
{
    public class DeleteExpenseTests : TestBench
    {
        [Fact]
        public async Task GivenUnusedExpenseExists_WhenDeleted_WillSucceed()
        {
            var expense = await CreateVariableExpenseAsync();

            var action = () => app.DeleteExpenseAsync(expense.Id);

            await action.Should().NotThrowAsync();
        }
    }
}
