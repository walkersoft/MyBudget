using FluentAssertions;
using MyBudget.Application.Entities;

namespace MyBudget.Application.Tests.Features.Categories
{
    public sealed class EditCategoryTests : TestBench
    {
        [Fact]
        public async Task GivenExistingCategoryWasEdited_WhenUpdated_WillSucceed()
        {
            var category = await CreateCategoryAsync();

            category.Name = "New Category Name";
            var action = async () => await app.UpdateCategoryAsync(category);

            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GivenExistingCategoryWasEdited_WhenUpdated_HasUpdatedData()
        {
            var newCategoryName = "New Category Name";
            var category = await CreateCategoryAsync();

            category.Name = newCategoryName;
            category = await app.UpdateCategoryAsync(category);
            
            category.Name.Should().Be(newCategoryName);
        }

        [Fact]
        public async Task GivenCategoryDoesNotExist_WhenUpdated_ThrowsException()
        {
            var category = new ExpenseCategory()
            {
                Id = Guid.NewGuid(),
                Name = "New Category"
            };

            var action = async () => await app.UpdateCategoryAsync(category);

            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}
