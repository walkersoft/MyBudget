using FluentAssertions;

namespace MyBudget.Application.Tests.Features.Categories
{
    public sealed class GetCategoriesTest : TestBench
    {
        [Fact]
        public async Task GivenNoCategoriesSaved_WhenFetched_ReturnsNoCategories()
        {
            var categories = await app.GetAllCategoriesAsync();

            categories.Count().Should().Be(0);
        }
    }
}
