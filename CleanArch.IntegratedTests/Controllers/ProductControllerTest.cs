using CleanArch.Application.UseCases.Product.Query.List;
using FluentAssertions;
using System.Net.Http.Json;

namespace CleanArch.IntegratedTests.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public async Task GetProducts_ReturnsOkAndList()
        {
            using var factory = new CustomWebApplicationFactory();
            using var client = factory.CreateClient();
            
            var response = await client.GetAsync("/api/v1/product");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<ListProductOutput>();
            content.Should().NotBeNull();
            content!.Products.Count().Should().BeGreaterThan(0);
        }
    }
}
