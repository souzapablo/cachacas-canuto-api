namespace CachacasCanuto.UnitTests.Services.Products
{
    public class ProductServiceTests : TestsConfiguration
    {
        private readonly Mock<IProductHttpService> _productHttpServiceMock = new();
        private readonly Mock<MessageHandler> _messageMock = new();
        private readonly ProductService _productService;
        private readonly List<ExternalProductViewModel> _products;

        public ProductServiceTests()
        {
            _productService = new(_productHttpServiceMock.Object, _messageMock.Object, _mapper);
            
            ExternalProductViewModel cachacinha = new()
            {
                Id = "IdCachacinha",
                Name = "Cachacinha",
                AlcooholContent = 27m
            };

            ExternalProductViewModel canaDaBoa = new()
            {
                Id = "IdCanaDaBoa",
                Name = "Cana da boa",
                AlcooholContent = 11m
            };

            ExternalProductViewModel singleton = new()
            {
                Id = "IdSingleton",
                Name = "Singleton",
                AlcooholContent = 80m
            };

            _products = new()
            {
                cachacinha, canaDaBoa, singleton
            };
        }

        #region Get All Products

        [Theory]
        [MemberData(nameof(ProductTestData.GetAllValidData),
        MemberType = typeof(ProductTestData))]
        public async Task Given_ValidData_When_QueryIsExecuted_Should_ReturnProducts(string name, decimal startContent, decimal endContent)
        {
            // Arrange
            _productHttpServiceMock.Setup(x => x.GetAllProducts())
                .ReturnsAsync(_products);

            // Act
            var sut = await _productService.GetAllProductsAsync(name, startContent, endContent);

            // Assert
            sut.Should().BeOfType<List<ProductViewModel>?>();
            sut.Should().HaveCount(1);
            _messageMock.Object.HasMessage.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ProductTestData.GetAllInvalidData),
        MemberType = typeof(ProductTestData))]
        public async Task Given_InvalidData_When_QueryIsExecuted_Should_ReturnEmptyList(string name, decimal startContent, decimal endContent)
        {
            // Arrange
            _productHttpServiceMock.Setup(x => x.GetAllProducts())
                .ReturnsAsync(_products);

            // Act
            var sut = await _productService.GetAllProductsAsync(name, startContent, endContent);

            // Assert
            sut.Should().BeOfType<List<ProductViewModel>?>();
            sut.Should().HaveCount(0);
            _messageMock.Object.HasMessage.Should().BeFalse();
        }

        [Fact]
        public async Task Given_AnyData_When_ProductHttpServiceReturnNull_Should_ReturnMessage()
        {
            // Act
            var sut = await _productService.GetAllProductsAsync("teste", 1m, 2m);

            // Assert
            sut.Should().BeNull();
            _messageMock.Object.Messages.Should().Contain(x => x.Message == "Não foi possível conectar com o servidor.");
        }

        #endregion

        #region GetProductById

        [Fact]
        public async Task Given_AValidId_When_QueryIsExecuted_Should_ReturnProduct()
        {
            // Arrange
            _productHttpServiceMock.Setup(x => x.GetProductById("IdCachacinha"))
                .ReturnsAsync(_products[0]);

            // Act
            var sut = await _productService.GetProductByIdAsync(_products[0].Id);

            // Assert
            sut.Should().BeOfType<ProductViewModel?>();
            _messageMock.Object.HasMessage.Should().BeFalse();
        }

        [Fact]
        public async Task Given_AnInvalidId_When_QueryIsExecuted_Should_ReturnMessage()
        {
            // Act
            var sut = await _productService.GetProductByIdAsync(_products[0].Id);

            // Assert
            sut.Should().BeNull();
            _messageMock.Object.Messages.Should().Contain(x => x.Message == $"Produto com o Id IdCachacinha não encontrado.");
        }

        #endregion

    }


}
