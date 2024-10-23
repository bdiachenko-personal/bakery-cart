using Bakery.Application.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Bakery.Application.Common.Constants;
using Bakery.Infrastructure.Data;

namespace Bakery.IntegrationTests
{
    public class ShoppingCartControllerTests(WebApplicationFactory<Program> factory)
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task Checkout_ShouldReturnCorrectTotal_WhenAddingItemsWithoutSales_1()
        {
            var result = await CheckoutAndGetResult(
                new List<(string, int)>
                {
                    ("Cookie", 1),
                    ("Brownie", 4),
                    ("Key Lime Cheesecake", 1)
                },
                new DateTime(2024, 10, 21)
            );

            result.Should().NotBeNull();
            result.Sum.Should().Be(16.25m);
        }

        [Fact]
        public async Task Checkout_ShouldReturnCorrectTotal_WhenAddingItemsWithoutSales_2()
        {
            var result = await CheckoutAndGetResult(
                new List<(string, int)>
                {
                    ("Cookie", 8)
                },
                new DateTime(2024, 10, 21)
            );

            result.Should().NotBeNull();
            result.Sum.Should().Be(8.5m);
        }

        [Fact]
        public async Task Checkout_ShouldReturnCorrectTotal_WhenAddingItemsWithoutSales_3()
        {
            var result = await CheckoutAndGetResult(
                new List<(string, int)>
                {
                    ("Cookie", 1),
                    ("Brownie", 1),
                    ("Key Lime Cheesecake", 1),
                    ("Mini Gingerbread Donut", 2)
                },
                new DateTime(2024, 10, 21)
            );

            result.Should().NotBeNull();
            result.Sum.Should().Be(12.25m);
        }

        [Fact]
        public async Task Checkout_ShouldReturnCorrectTotal_WhenAddingItemsWithSales()
        {
            var result = await CheckoutAndGetResult(
                new List<(string, int)>
                {
                    ("Cookie", 8),
                    ("Key Lime Cheesecake", 4)
                },
                new DateTime(2022, 10, 1)
            );

            result.Should().NotBeNull();
            result.Sum.Should().Be(32.50m);
        }

        [Fact]
        public async Task Checkout_ShouldReturnBadRequest_WhenProductIsUnrecognized()
        {
            var cartDto = new CartDTO
            {
                Items = new List<CartItemDTO>
                {
                    new() { ProductId = Guid.NewGuid(), Quantity = 1 },
                },
                DateCreated = DateTime.Now
            };

            var response = await _client.PostAsJsonAsync("/api/shoppingcart/checkout", cartDto);

            var errorMessage = await response.Content.ReadAsStringAsync();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            errorMessage.Should().Contain(ErrorMessages.UnrecognizedProduct);
        }
        
        private async Task<CheckoutDTO> CheckoutAndGetResult(List<(string productName, int quantity)> items,
            DateTime dateCreated)
        {
            var cartDto = new CartDTO
            {
                Items = items.Select(i => new CartItemDTO
                {
                    ProductId = SeedData.Products.Single(p => p.Name == i.productName).Id,
                    Quantity = i.quantity
                }).ToList(),
                DateCreated = dateCreated
            };

            var response = await _client.PostAsJsonAsync("/api/shoppingcart/checkout", cartDto);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<CheckoutDTO>();
        }
    }
}