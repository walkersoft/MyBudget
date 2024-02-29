using FluentAssertions;

namespace MyBudget.Application.Tests.Features.Categories
{
    public sealed class GetCategoryTests : TestBench
    {
        [Fact]
        public async Task GivenCategoryExists_WhenFetched_WillSucceed()
        {
            var category = await CreateCategoryAsync();

            var action = async () => await app.GetCategoryAsync(category.Id);

            await action.Should().NotThrowAsync();
        }
    }
}
