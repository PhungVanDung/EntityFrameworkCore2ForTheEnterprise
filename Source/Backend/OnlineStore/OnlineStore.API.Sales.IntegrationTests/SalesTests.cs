using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineStore.API.Common.IntegrationTests;
using OnlineStore.API.Common.IntegrationTests.Helpers;
using Xunit;

namespace OnlineStore.API.Sales.IntegrationTests
{
    public class SalesTests : IClassFixture<TestFixture<Startup>>
    {
        readonly HttpClient Client;

        public SalesTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TestSearchOrdersAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/SearchOrder",
                Body = new
                {
                    PageSize = 10,
                    PageNumber = 1
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestSearchOrdersByCurrencyAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/SearchOrder",
                Body = new
                {
                    PageSize = 10,
                    PageNumber = 1,
                    CurrencyID = 1
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestSearchOrdersByCustomerAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/SearchOrder",
                Body = new
                {
                    PageSize = 10,
                    PageNumber = 1,
                    CustomerID = 1
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestSearchOrdersByEmployeeAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/SearchOrder",
                Body = new
                {
                    EmployeeID = 1
                }
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrderByIdAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/Order/{0}", 1)
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetOrderByNonExistingIdAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/Order/{0}", 0)
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = "/api/v1/Sales/CreateOrderRequest"
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestGetCreateOrderRequestAsWarehouseOperatorAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWarehouseOperatorAsync();
            var request = new
            {
                Url = "/api/v1/Sales/CreateOrderRequest"
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task TestPostOrderAsCustomerAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/v1/Sales/Order",
                Body = new
                {
                    UserName = "jameslogan@walla.com",
                    Password = "wolverine",
                    CardHolderName = "James Logan",
                    IssuingNetwork = "Visa",
                    CardNumber = "4024007164051145",
                    ExpirationDate = new DateTime(2024, 6, 1),
                    Cvv = "987",
                    Total = 29.99m,
                    CustomerID = 1,
                    CurrencyID = "USD",
                    PaymentMethodID = new Guid("7671A4F7-A735-4CB7-AAB4-CF47AE20171D"),
                    Comments = "Order from integration tests",
                    Details = new[]
                    {
                        new
                        {
                            ProductID = 1,
                            Quantity = 1
                        }
                    }
                }
            };

            var token = await TokenHelper
                .GetOnlineStoreTokenAsync(request.Body.UserName, request.Body.Password);

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client
                .PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TestCloneOrderAsCustomerAsync()
        {
            // Arrange
            var token = await TokenHelper.GetTokenForWolverineAsync();
            var request = new
            {
                Url = string.Format("/api/v1/Sales/CloneOrder/{0}", 1)
            };

            // Act
            Client.SetBearerToken(token.AccessToken);

            var response = await Client.GetAsync(request.Url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
