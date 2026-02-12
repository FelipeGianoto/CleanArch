using CleanArch.Application.UseCases.Category.Query.List;
using FluentAssertions;
using System.Net.Http.Json;

namespace CleanArch.IntegratedTests.Controllers
{
    public class CategoryControllerTest
    {
        [Fact]
        public async Task GetCategories_ReturnsOkAndList()
        {
            using var factory = new CustomWebApplicationFactory();
            using var client = factory.CreateClient();

            var response = await client.GetAsync("/api/v1/category");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<ListCategoryOutput>();
            content.Should().NotBeNull();
            content!.Categories.Count().Should().BeGreaterThan(0);
        }
    }
}
