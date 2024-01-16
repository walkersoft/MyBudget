using FluentAssertions;

namespace MyBudget.Application.Tests.Features.Categories
{
    public sealed class DeleteCategoryTests : TestBench
    {
        [Fact]
        public async Task GivenExistingCategory_WhenDeleted_WillSucceed()
        {
            var category = await CreateCategoryAsync();

            await app.DeleteCategoryAsync(category.Id);
            var categories = await app.GetAllCategoriesAsync();

            categories.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenExistingCategoryAssignedToExpense_WhenDeleted_WillThrowException()
        {
            var category = await CreateCategoryAsync();
            await CreateVariableExpenseAsync(category.Id);

            var action = async () => await app.DeleteCategoryAsync(category.Id);

            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}
