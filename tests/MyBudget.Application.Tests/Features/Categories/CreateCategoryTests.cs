using FluentAssertions;

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
    }
}
