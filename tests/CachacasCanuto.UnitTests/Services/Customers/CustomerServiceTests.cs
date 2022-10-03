using CachacasCanuto.Application.Common;
using CachacasCanuto.Application.Services;
using CachacasCanuto.Infrastructure.ExternalResources.HttpServices.Interfaces;

namespace CachacasCanuto.UnitTests.Services.Customers
{
    public class CustomerServiceTests : TestsConfiguration
    {
        private readonly Mock<ICustomerHttpService> _customerHttpServiceMock = new();
        private readonly Mock<MessageHandler> _messageMock = new();
        private readonly CustomerService _customerService;
        private readonly List<ExternalCustomerViewModel> _customers;

        public CustomerServiceTests()
        {
            _customerService = new(_customerHttpServiceMock.Object, _messageMock.Object, _mapper);

            ExternalCustomerViewModel hamilton = new()
            {
                Id = "IdHamilton",
                Name = "Lewis Hamilton",
                BirthDate = new DateTime(1985, 01, 07)
            };

            ExternalCustomerViewModel casimiro = new()
            {
                Id = "IdCasimiro",
                Name = "Casimiro",
                BirthDate = new DateTime(1993, 10, 20)
            };

            ExternalCustomerViewModel donJuan = new()
            {
                Id = "IdDonJuan",
                Name = "Don Juan",
                BirthDate = new DateTime(1819, 10, 27)
            };

            _customers = new() { hamilton, casimiro, donJuan };
        }

        #region Get All Customers

        [Theory]
        [MemberData(nameof(CustomerTestData.GetAllValidData),
        MemberType = (typeof(CustomerTestData)))]
        public async Task Given_ValidData_When_QueryIsExecuted_Should_ReturnCustomers(string name, DateTime startDate, DateTime endDate)
        {
            // Arrange
            _customerHttpServiceMock.Setup(x => x.GetAllCustomers())
                .ReturnsAsync(_customers);

            // Act
            var sut = await _customerService.GetAllCustomersAsync(name, startDate, endDate);

            // Assert
            sut.Should().BeOfType<List<CustomerViewModel>?>();
            sut.Should().HaveCount(1);
            _messageMock.Object.HasMessage.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(CustomerTestData.GetAllInvalidData),
        MemberType = (typeof(CustomerTestData)))]
        public async Task Given_InvalidData_When_QueryIsExecuted_Should_ReturnEmpty(string name, DateTime startDate, DateTime endDate)
        {
            // Arrange
            _customerHttpServiceMock.Setup(x => x.GetAllCustomers())
                .ReturnsAsync(_customers);

            // Act
            var sut = await _customerService.GetAllCustomersAsync(name, startDate, endDate);

            // Assert
            sut.Should().BeOfType<List<CustomerViewModel>?>();
            sut.Should().HaveCount(0);
            _messageMock.Object.HasMessage.Should().BeFalse();
        }

        [Fact]
        public async Task Given_AnyData_When_CustomerHttpServiceReturnNull_Should_ReturnMessage()
        {
            // Act
            var sut = await _customerService.GetAllCustomersAsync("teste", DateTime.Now, DateTime.Now);

            // Assert
            sut.Should().BeNull();
            _messageMock.Object.Messages.Should().Contain(x => x.Message == "Não foi possível conectar com o servidor.");
        }

        #endregion

        #region Get Customers By Id

        [Fact]
        public async Task Given_AValidId_When_QueryIsExecuted_Should_ReturnCustomer()
        {
            // Arrange
            _customerHttpServiceMock.Setup(x => x.GetCustomerById("IdHamilton"))
                .ReturnsAsync(_customers[0]);

            // Act
            var sut = await _customerService.GetCustomerByIdAsync(_customers[0].Id);

            // Assert
            sut.Should().BeOfType<CustomerViewModel?>();
            _messageMock.Object.HasMessage.Should().BeFalse();
        }

        [Fact]
        public async Task Given_AnInvalidId_When_QueryIsExecuted_Should_ReturnMessage()
        {
            // Act
            var sut = await _customerService.GetCustomerByIdAsync(_customers[0].Id);

            // Assert
            sut.Should().BeNull();
            _messageMock.Object.Messages.Should().Contain(x => x.Message == $"Cliente com o Id IdHamilton não encontrado.");
        }

        #endregion
    }
}
