using NSubstitute;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;
using RESERVATION_SYSTEM.Domain.Services.customer;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;
using RESERVATION_SYSTEM.Domain.Services.reservation;
using RESERVATION_SYSTEM.Domain.Services.service;
using RESERVATION_SYSTEM.Domain.Tests.DataBuilder;
using System.Linq.Expressions;

namespace RESERVATION_SYSTEM.Domain.Tests.customer
{
    [TestClass]
    public class CustomersServiceTest
    {
        private CustomerService Service { get; set; } = default!;
        private ReservationService ReservationService { get; set; } = default!;
        private HistoryReservationService HistoryReservationService { get; set; } = default!;
        private ServiceService ServiceService { get; set; } = default!;
        private IQueryWrapper QueryWrapper { get; set; } = default!;
        private IGenericRepository<Customer> Repository { get; set; } = default!;
        private IGenericRepository<Reservation> ReservationRepository { get; set; } = default!;
        private CustomerDtoBuilder CustomerDtoBuilder { get; set; } = default!;
        private FieldFilterBuilder FieldFilterBuilder { get; set; } = default!;
        private CustomerBuilder CustomerBuilder { get; set; } = default!;

        [TestInitialize]
        public void Initialize()
        {
            QueryWrapper = Substitute.For<IQueryWrapper>();
            Repository = Substitute.For<IGenericRepository<Customer>>();
            ReservationRepository = Substitute.For<IGenericRepository<Reservation>>();

            ReservationService = new(
                ReservationRepository,
                QueryWrapper,
                HistoryReservationService,
                ServiceService
            );

            Service = new(
                Repository,
                QueryWrapper,
                ReservationService
            );

            CustomerDtoBuilder = new();
            FieldFilterBuilder = new();
            CustomerBuilder = new();
        }

        [TestMethod]
        public async Task ObtainCustomerAsync_Ok()
        {
            //Arrange
            List<FieldFilter> customerFilter = [FieldFilterBuilder.Build()];

            List<CustomerDto> customers =
            [
                CustomerDtoBuilder
                    .Build()
            ];

            QueryWrapper
                .QueryAsync<CustomerDto>(
                    ItemsMessageConstants.GetCustomers.GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, customerFilter)]
                )
                .ReturnsForAnyArgs(customers);

            //Act
            List<CustomerDto> response = await Service.ObtainCustomerAsync(customerFilter);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Count);
            await QueryWrapper.ReceivedWithAnyArgs(1)
                .QueryAsync<CustomerDto>(
                    Arg.Any<string>(),
                    Arg.Any<object>(),
                    Arg.Any<object[]>()
                );
        }

        [TestMethod]
        public async Task CreateCustomerAsync_Ok()
        {
            //Arrange
            string name = "John";
            string email = "John@gmail.com";
            string phone = "3215974569";

            Customer customer = CustomerBuilder.Build();
            List<Customer> listCustomer = [];

            Repository.GetAsync(
                customer => customer.Name == name
            )
            .ReturnsForAnyArgs(listCustomer);

            Repository.AddAsync(customer)
                .ReturnsForAnyArgs(customer);

            //Act
            await Service.CreateCustomerAsync(name, email, phone);

            //Assert            
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Customer, bool>>>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(
                    Arg.Any<Customer>()
                );
        }

        [TestMethod]
        public async Task CreateCustomerAsync_Failed()
        {
            //Arrange
            string name = "John";
            string email = "John@gmail.com";
            string phone = "3215974569";

            Customer customer = CustomerBuilder.Build();
            List<Customer> listCustomer = [customer];

            Repository.GetAsync(
                customer => customer.Name == name
            )
            .ReturnsForAnyArgs(listCustomer);

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.CreateCustomerAsync(name, email, phone);
            });

            //Assert
            Assert.AreEqual(MessagesExceptions.NameCustomerNotValid, ex.Message);
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Customer, bool>>>()
                );
        }

        [TestMethod]
        public async Task UpdateCustomerAsync_Ok()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string name = "John";
            string email = "John@gmail.com";
            string phone = "3215974569";

            Customer customer = CustomerBuilder.Build();
            List<Customer> listCustomer = [];

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(customer);

            Repository.GetAsync(
                customer => customer.Name == name
            )
            .ReturnsForAnyArgs(listCustomer);

            Repository.UpdateAsync(customer)
                .ReturnsForAnyArgs(customer);

            //Act
            await Service.UpdateCustomerAsync(id, name, email, phone);

            //Assert            
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Customer, bool>>>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(
                    Arg.Any<Customer>()
                );
        }

        [TestMethod]
        public async Task UpdateCustomerAsync_Failed()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            string name = "John";
            string email = "John@gmail.com";
            string phone = "3215974569";

            Customer customer = CustomerBuilder.Build();
            List<Customer> listCustomer = [customer];

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(customer);

            Repository.GetAsync(
                customer => customer.Name == name
            )
            .ReturnsForAnyArgs(listCustomer);

            //Act
            AppException ex = await Assert.ThrowsExceptionAsync<AppException>(async () =>
            {
                await Service.UpdateCustomerAsync(id, name, email, phone);
            });

            //Assert            
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Customer, bool>>>()
                );
        }

        [TestMethod]
        public async Task DeleteCustomerAsync_Failed()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            Customer customer = CustomerBuilder.Build();

            Repository.GetByIdAsync(id)
               .ReturnsForAnyArgs(customer);

            Repository.DeleteAsync(customer)
                .Returns(Task.CompletedTask);

            //Act
            await Service.DeleteCustomerAsync(id);

            //Assert            
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
               .DeleteAsync(Arg.Any<Customer>());
        }
    }
}
