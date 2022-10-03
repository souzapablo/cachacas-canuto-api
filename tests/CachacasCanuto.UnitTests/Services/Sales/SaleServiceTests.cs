using CachacasCanuto.Application.ViewModels.Sales;
using CachacasCanuto.Core.Helpers.Pagination;

namespace CachacasCanuto.UnitTests.Services.Sales
{
    public class SaleServiceTests : TestsConfiguration
    {
        private readonly Mock<ISaleHttpService> _saleHttpServiceMock = new();
        private readonly Mock<ICustomerService> _customerServiceMock = new();
        private readonly Mock<IProductService> _productServiceMock = new();
        private readonly Mock<MessageHandler> _messageMock = new();
        private readonly SaleService _saleService;
        private List<ExternalSaleViewModel>? _sales;

        public SaleServiceTests()
        {
            _saleService = new(_saleHttpServiceMock.Object, _customerServiceMock.Object, _productServiceMock.Object, _messageMock.Object, _mapper);
            GetSaleData();
        }

        #region Get sales

        [Theory]
        [MemberData(nameof(SaleTestData.GetAllValidData),
        MemberType = typeof(SaleTestData))]
        public async Task Given_ValidData_When_QueryIsExecuted_Should_ReturnSales(DateTime startDate, DateTime endDate, string customerName, List<string> customersId, string productName, List<string> productsIds, int quantityPerPage, int currentPage)
        {
            // Arrange
            _saleHttpServiceMock.Setup(x => x.GetAllSalesAsync())
                .ReturnsAsync(_sales);

            _customerServiceMock.Setup(x => x.GetCustomersIdsByNameAsync(customerName))
                .ReturnsAsync(customersId);

            _productServiceMock.Setup(x => x.GetProductsIdsByNameAsync(productName))
                .ReturnsAsync(productsIds);

            // Act
            var sut = await _saleService.GetSalesAsync(startDate, endDate, customerName, productName, quantityPerPage, currentPage);

            // Assert
            sut.Should().BeOfType<Pagination<SaleViewModel>?>();
            sut?.CurrentPage.Should().Be(1);
            sut?.QuantityPerPage.Should().Be(10);
            sut?.Data.Should().HaveCount(1);
        }

        #endregion

        private void GetSaleData()
        {
            ExternalSaleViewModel firstSale = new()
            {
                Date = new DateTime(2010, 09, 10),
                CustomerId = "IdHamilton",
                Itens = new()
                {
                    new ExternalSaleItemViewModel
                    {
                        Id = "IdCachacinha",
                        UnitPrice = 5m,
                        Quantity = 3
                    }
                }
            };

            ExternalSaleViewModel secondSale = new()
            {
                Date = new DateTime(1999, 01, 01),
                CustomerId = "IdCasimiro",
                Itens = new()
                {
                    new ExternalSaleItemViewModel
                    {
                        Id = "IdSingleton",
                        UnitPrice = 10m,
                        Quantity = 2
                    }
                }
            };

            ExternalSaleViewModel thirdSale = new()
            {
                Date = new DateTime(1880, 12, 25),
                CustomerId = "IdDonJuan",
                Itens = new()
                {
                    new ExternalSaleItemViewModel
                    {
                        Id = "IdCanaBraba",
                        UnitPrice = 20m,
                        Quantity = 4
                    }
                }
            };

            _sales = new() { firstSale, secondSale, thirdSale };
        }
    }
}
