using FluentAssertions;
using FluentValidation;

namespace MyBudget.Application.Tests.Features.Categories
{
    public sealed class CreateCategoryTests : TestBench
    {
        [Fact]
        public async Task ValidCategory_WhenCreated_HasNewId()
        {
            var category = await CreateCategoryAsync();

            category.Should().NotBeNull();
            category.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ValidCategoryAssignedToExpense_WhenFetched_ContainsExpense()
        {
            var category = await CreateCategoryAsync();

            await CreateVariableExpenseAsync(category.Id);
            var categories = await app.GetAllCategoriesAsync();
            var expense = categories.First().Expenses[0];

            categories.First().Expenses.Count.Should().Be(1);
            expense.ExpenseCategoryId.Should().Be(category.Id);
        }

        [Fact]
        public async Task CategoryWithoutName_WhenCreated_ThrowsException()
        {
            var action = async () => await app.CreateCategoryAsync("");
            await action.Should().ThrowAsync<ValidationException>();
        }
    }
}
