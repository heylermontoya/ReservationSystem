using NSubstitute;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;
using RESERVATION_SYSTEM.Domain.Tests.DataBuilder;

namespace RESERVATION_SYSTEM.Domain.Tests.historyReservation
{
    [TestClass]
    public class HistoryReservationServiceTest
    {
        private HistoryReservationService Service { get; set; } = default!;
        private IQueryWrapper QueryWrapper { get; set; } = default!;
        private IGenericRepository<HistoryReservation> Repository { get; set; } = default!;
        private HistoryReservationDtoBuilder HistoryReservationDtoBuilder { get; set; } = default!;
        private HistoryReservationBuilder HistoryReservationBuilder { get; set; } = default!;
        private FieldFilterBuilder FieldFilterBuilder { get; set; } = default!;

        [TestInitialize]
        public void Initialize()
        {
            QueryWrapper = Substitute.For<IQueryWrapper>();
            Repository = Substitute.For<IGenericRepository<HistoryReservation>>();

            Service = new(
                Repository,
                QueryWrapper
            );

            HistoryReservationDtoBuilder = new();
            HistoryReservationBuilder = new();
            FieldFilterBuilder = new();
        }

        [TestMethod]
        public async Task ObtainHistoryReservationDtoAsync_Ok()
        {
            //Arrange
            List<FieldFilter> filter = [FieldFilterBuilder.Build()];

            List<HistoryReservationDto> listHistoryReservationDto = [
                HistoryReservationDtoBuilder.Build()
            ];

            QueryWrapper
                .QueryAsync<HistoryReservationDto>(
                    ItemsMessageConstants.GetHistoryReservation.GetDescription(),
                    new
                    { },
                    [FieldFilterHelper.BuildQuery(addWhereClause: true, filter)]
                )
                .ReturnsForAnyArgs(listHistoryReservationDto);

            //Act
            List<HistoryReservationDto> response = await Service.ObtainHistoryReservationDtoAsync(filter);

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(1, response.Count);
            await QueryWrapper.ReceivedWithAnyArgs(1)
                .QueryAsync<HistoryReservationDto>(
                    Arg.Any<string>(),
                    Arg.Any<object>(),
                    Arg.Any<object[]>()
                );
        }

        [TestMethod]
        public async Task CreateHistoryReservationAsync_Ok()
        {
            //Arrange
            Guid reservationID = Guid.NewGuid();
            HistoryReservation historyReservation = HistoryReservationBuilder.Build();

            Repository.AddAsync(historyReservation)
                .ReturnsForAnyArgs(historyReservation);

            //Act
            await Service.CreateHistoryReservationAsync(reservationID, ReservationStatus.Confirmed);

            //Assert                       
            await Repository.ReceivedWithAnyArgs(1)
                .AddAsync(
                    Arg.Any<HistoryReservation>()
                );
        }
    }
}
