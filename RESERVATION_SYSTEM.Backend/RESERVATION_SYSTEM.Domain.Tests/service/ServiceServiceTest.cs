using NSubstitute;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;
using RESERVATION_SYSTEM.Domain.Services.service;
using RESERVATION_SYSTEM.Domain.Tests.DataBuilder;
using System.Linq.Expressions;

namespace RESERVATION_SYSTEM.Domain.Tests.service
{
    [TestClass]
    public class ServiceServiceTest
    {
        private ServiceService Service { get; set; } = default!;
        private IQueryWrapper QueryWrapper { get; set; } = default!;
        private IGenericRepository<Service> Repository { get; set; } = default!;
        private FieldFilterBuilder FieldFilterBuilder { get; set; } = default!;
        private ServiceDtoBuilder ServiceDtoBuilder { get; set; } = default!;
        private ServiceBuilder ServiceBuilder { get; set; } = default!;

        [TestInitialize]
        public void Initialize()
        {
            QueryWrapper = Substitute.For<IQueryWrapper>();
            Repository = Substitute.For<IGenericRepository<Service>>();

            Service = new(
                Repository,
                QueryWrapper
            );

            FieldFilterBuilder = new();
            ServiceDtoBuilder = new();
            ServiceBuilder = new();
        }

        [TestMethod]
        public async Task ObtainServiceAsync_Ok()
        {
            //Arrange
            List<FieldFilter> customerFilter = [FieldFilterBuilder.Build()];

            List<ServiceDto> services = [ServiceDtoBuilder.Build()];

            QueryWrapper
                .QueryAsync<ServiceDto>(
                    ItemsMessageConstants.GetServices.GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, customerFilter)]
                )
                .ReturnsForAnyArgs(services);

            //Act
            List<ServiceDto> response = await Service.ObtainServiceAsync(customerFilter);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Count);
            await QueryWrapper.ReceivedWithAnyArgs(1)
                .QueryAsync<ServiceDto>(
                    Arg.Any<string>(),
                    Arg.Any<object>(),
                    Arg.Any<object[]>()
                );
        }

        [TestMethod]
        public async Task CreateServiceAsync_Ok()
        {
            //Arrange
            string name = "sencilla";
            string description = "sencilla";
            float price = 1;
            int capacity = 1;

            Service service = ServiceBuilder.Build();
            IEnumerable<Service> listService = [];

            Repository.GetAsync(
                service => service.Name == name
            ).ReturnsForAnyArgs(listService); ;

            Repository.AddAsync(service)
                .ReturnsForAnyArgs(service);

            //Act
            await Service.CreateServiceAsync(name, description, price, capacity);

            //Assert            
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Service, bool>>>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(
                    Arg.Any<Service>()
                );
        }

        [TestMethod]
        public async Task CreateServiceAsync_Failed()
        {
            //Arrange
            string name = "sencilla";
            string description = "sencilla";
            float price = 1;
            int capacity = 1;

            Service service = ServiceBuilder.Build();
            IEnumerable<Service> listService = [service];

            Repository.GetAsync(
                service => service.Name == name
            ).ReturnsForAnyArgs(listService); ;

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.CreateServiceAsync(name, description, price, capacity);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.NameServiceNotValid, ex.Message);
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Service, bool>>>()
                );
        }

        [TestMethod]
        public async Task UpdateServiceAsync_Ok()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string name = "sencilla";
            string description = "sencilla";
            float price = 1;
            int capacity = 1;

            Service service = ServiceBuilder.Build();
            IEnumerable<Service> listService = [];

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(service);

            Repository.GetAsync(
                service => service.Name == name
            ).ReturnsForAnyArgs(listService); ;

            Repository.UpdateAsync(service)
                .ReturnsForAnyArgs(service);

            //Act
            await Service.UpdateServiceAsync(id, name, description, price, capacity);

            //Assert
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Service, bool>>>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(
                    Arg.Any<Service>()
                );
        }

        [TestMethod]
        public async Task UpdateServiceAsync_Failed()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string name = "sencilla";
            string description = "sencilla";
            float price = 1;
            int capacity = 1;

            Service service = ServiceBuilder.Build();
            IEnumerable<Service> listService = [service];

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(service);

            Repository.GetAsync(
                service => service.Name == name
            ).ReturnsForAnyArgs(listService); ;

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.UpdateServiceAsync(id, name, description, price, capacity);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.NameServiceNotValid, ex.Message);
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Service, bool>>>()
                );
        }

        [TestMethod]
        public async Task UpdateServiceNotAvailableAsync_Ok()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            bool available = true;

            Service service = ServiceBuilder.Build();

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(service);

            Repository.UpdateAsync(service)
                .ReturnsForAnyArgs(service);

            //Act
            await Service.UpdateServiceNotAvailableAsync(id, available);

            //Assert
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(
                    Arg.Any<Service>()
                );
        }

        [TestMethod]
        public async Task DeleteServiceAsync_Failed()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            Service service = ServiceBuilder.Build();

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(service);

            Repository.DeleteAsync(service)
                .Returns(Task.CompletedTask);

            //Act
            await Service.DeleteServiceAsync(id);

            //Assert            
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
               .DeleteAsync(Arg.Any<Service>());
        }
    }
}
