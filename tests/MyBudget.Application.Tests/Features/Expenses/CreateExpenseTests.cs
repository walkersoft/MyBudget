﻿using FluentAssertions;

namespace MyBudget.Application.Tests.Features.Expenses
{
    public sealed class CreateExpenseTests : TestBench
    {
        [Fact]
        public async Task ValidExpense_WhenCreated_HasNewId()
        {
            var expense = await app.CreateExpenseAsync();

            expense.Should().NotBeNull();
            expense.Id.Should().NotBeEmpty();
        }
    }
}