using FluentAssertions;

namespace MyBudget.Application.Tests.Features.Categories
{
    public sealed class DeleteCategoryTests : TestBench
    {
        [Fact]
        public async Task GivenExistingCategory_WhenUnassigned_CanBeDeleted()
        {
            var expense = await CreateCategoryAsync();

            await app.DeleteCategoryAsync(expense.Id);
            var categories = await app.GetAllCategoriesAsync();

            categories.Should().BeEmpty();
        }
    }
}
