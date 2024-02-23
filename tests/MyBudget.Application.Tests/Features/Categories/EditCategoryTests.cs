using FluentAssertions;

namespace MyBudget.Application.Tests.Features.Categories
{
    public sealed class EditCategoryTests : TestBench
    {
        [Fact]
        public async Task GivenExistingCategoryWasEdited_WhenSaved_WillSucceed()
        {
            var category = await CreateCategoryAsync();

            category.Name = "New Category Name";
            var action = async () => await app.UpdateCategoryAsync(category);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GivenExistingCategoryWasEdited_WhenSaved_HasUpdatedData()
        {
            var newCategoryName = "New Category Name";
            var category = await CreateCategoryAsync();

            category.Name = newCategoryName;
            category = await app.UpdateCategoryAsync(category);
            
            category.Name.Should().Be(newCategoryName);
        }
    }
}
