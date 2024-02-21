using FluentAssertions;

namespace MyBudget.Application.Tests.Features.Categories
{
    public sealed class EditCategoryTests : TestBench
    {
        [Fact]
        public async Task GivenExistingCategoryWasEdited_WhenSaved_WillSucceed()
        {
            var newCategoryName = "New Category Name";
            var category = await CreateCategoryAsync();

            category.Name = newCategoryName;
            category = await app.UpdateCategoryAsync(category);
            
            category.Name.Should().Be(newCategoryName);
        }
    }
}
