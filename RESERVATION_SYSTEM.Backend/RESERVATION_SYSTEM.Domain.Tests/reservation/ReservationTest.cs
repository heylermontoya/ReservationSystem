using NSubstitute;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;
using RESERVATION_SYSTEM.Domain.Services.reservation;
using RESERVATION_SYSTEM.Domain.Services.service;
using RESERVATION_SYSTEM.Domain.Tests.DataBuilder;
using System.Linq.Expressions;

namespace RESERVATION_SYSTEM.Domain.Tests.reservation
{
    [TestClass]
    public class ReservationTest
    {
        private ReservationService Service { get; set; } = default!;
        private HistoryReservationService HistoryReservationService { get; set; } = default!;
        private ServiceService ServiceService { get; set; } = default!;
        private IQueryWrapper QueryWrapper { get; set; } = default!;
        private IGenericRepository<Reservation> Repository { get; set; } = default!;
        private FieldFilterBuilder FieldFilterBuilder { get; set; } = default!;
        private ReservationDtoBuilder ReservationDtoBuilder { get; set; } = default!;
        private ReservationBuilder ReservationBuilder { get; set; } = default!;
        private ServiceBuilder ServiceBuilder { get; set; } = default!;
        private IGenericRepository<HistoryReservation> HistoryReservationRepository { get; set; } = default!;
        private IGenericRepository<Service> ServiceRepository { get; set; } = default!;


        [TestInitialize]
        public void Initialize()
        {
            QueryWrapper = Substitute.For<IQueryWrapper>();
            Repository = Substitute.For<IGenericRepository<Reservation>>();
            HistoryReservationRepository = Substitute.For<IGenericRepository<HistoryReservation>>();
            ServiceRepository = Substitute.For<IGenericRepository<Service>>();

            HistoryReservationService = new(
                HistoryReservationRepository,
                QueryWrapper
            );

            ServiceService = new(
                ServiceRepository,
                QueryWrapper
            );

            Service = new(
                Repository,
                QueryWrapper,
                HistoryReservationService,
                ServiceService
            );

            FieldFilterBuilder = new();
            ReservationDtoBuilder = new();
            ReservationBuilder = new();
            ServiceBuilder = new();
        }

        [TestMethod]
        public async Task ObtainReservationAsync_Ok()
        {
            //Arrange
            List<FieldFilter> customerFilter = [FieldFilterBuilder.Build()];

            List<ReservationDto> reservations =
            [
                ReservationDtoBuilder
                    .Build()
            ];

            QueryWrapper
                .QueryAsync<ReservationDto>(
                     ItemsMessageConstants.GetReservation.GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, customerFilter)]
                )
                .ReturnsForAnyArgs(reservations);

            //Act
            List<ReservationDto> response = await Service.ObtainReservationAsync(customerFilter);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Count);
            await QueryWrapper.ReceivedWithAnyArgs(1)
                .QueryAsync<ReservationDto>(
                    Arg.Any<string>(),
                    Arg.Any<object>(),
                    Arg.Any<object[]>()
                );
        }

        [TestMethod]
        public async Task CreateReservationAsync_Ok()
        {
            //Arrange
            Guid customerID = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            int numberPeople = 1;
            float total = 1;

            Reservation reservation = ReservationBuilder.Build();

            Repository.AddAsync(reservation)
                .ReturnsForAnyArgs(reservation);

            Guid idService = Guid.NewGuid();

            Service service = ServiceBuilder.Build();

            ServiceRepository.GetByIdAsync(idService)
                .ReturnsForAnyArgs(service);
            //Act
            await Service.CreateReservationAsync(customerID, serviceID, startDate, endDate, numberPeople, total);

            //Assert                        
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(
                    Arg.Any<Reservation>()
                );
            await ServiceRepository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
        }

        [TestMethod]
        public async Task UpdateReservationAsync_Ok()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Guid serviceID = Guid.NewGuid();
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            int numberPeople = 1;
            float total = 1;

            Reservation reservation = ReservationBuilder.Build();

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(reservation);

            Guid idService = Guid.NewGuid();

            Service service = ServiceBuilder.Build();

            ServiceRepository.GetByIdAsync(idService)
                .ReturnsForAnyArgs(service);

            Repository.UpdateAsync(reservation)
               .ReturnsForAnyArgs(reservation);

            //Act
            await Service.UpdateReservationAsync(id, serviceID, startDate, endDate, numberPeople, total);

            //Assert
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await ServiceRepository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(
                    Arg.Any<Reservation>()
                );
        }

        [TestMethod]
        public async Task CancelReservationAsync_Ok()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            Reservation reservation = ReservationBuilder.Build();

            Repository.GetByIdAsync(id)
                .ReturnsForAnyArgs(reservation);

            Repository.UpdateAsync(reservation)
               .ReturnsForAnyArgs(reservation);

            Guid idService = Guid.NewGuid();

            Service service = ServiceBuilder.Build();

            ServiceRepository.GetByIdAsync(idService)
                .ReturnsForAnyArgs(service);

            //Act
            await Service.CancelReservationAsync(id);

            //Assert
            await Repository.ReceivedWithAnyArgs(1)
                .GetByIdAsync(
                    Arg.Any<Guid>()
                );
            await Repository.ReceivedWithAnyArgs(1)
                .UpdateAsync(
                    Arg.Any<Reservation>()
                );
        }

        [TestMethod]
        public async Task LiberateServicesAsync_Ok()
        {
            //Arrange
            Guid idCustomer = Guid.NewGuid();

            List<Reservation> listReservation = [ReservationBuilder.Build()];

            Repository.GetAsync(
                reservation => reservation.CustomerID == idCustomer &&
                reservation.State != ReservationStatus.Canceled.ToString() &&
                reservation.ServiceID != null
            )
                .ReturnsForAnyArgs(listReservation);

            Guid idService = Guid.NewGuid();

            Service service = ServiceBuilder.Build();
            IEnumerable<Service> listService = [];

            ServiceRepository.GetByIdAsync(idService)
                .ReturnsForAnyArgs(service);

            //Act
            await Service.LiberateServicesAsync(idCustomer);

            //Assert
            await Repository.ReceivedWithAnyArgs(1)
                .GetAsync(
                    Arg.Any<Expression<Func<Reservation, bool>>>()
                );
        }
    }
}
