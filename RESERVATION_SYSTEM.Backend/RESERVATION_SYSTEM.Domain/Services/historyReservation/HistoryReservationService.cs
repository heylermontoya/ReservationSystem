using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Domain.Services.historyReservation
{
    [DomainService]
    public class HistoryReservationService
    {
        protected readonly IGenericRepository<HistoryReservation> repository;
        protected readonly IQueryWrapper queryWrapper;
        public HistoryReservationService(
            IGenericRepository<HistoryReservation> repository,
            IQueryWrapper queryWrapper
        )
        {

            this.repository = repository;
            this.queryWrapper = queryWrapper;
        }

        public async Task<List<HistoryReservationDto>> ObtainHistoryReservationDtoAsync(IEnumerable<FieldFilter>? filters)
        {

            List<FieldFilter> listFilters = filters != null ? filters.ToList() : [];

            IEnumerable<HistoryReservationDto> historyReservations =
                await queryWrapper
                    .QueryAsync<HistoryReservationDto>(
                        ItemsMessageConstants.GetHistoryReservation
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(listFilters)
                    );

            return historyReservations.ToList();
        }

        public async Task CreateHistoryReservationAsync(
            Guid reservationID,
            ReservationStatus descriptionChange
        )
        {
            HistoryReservation historyReservation = new()
            {
                ReservationID = reservationID,
                DateChange = DateConverterHelper.ConvertDatetimeToLocalZone(DateTime.UtcNow),
                DescriptionChange = descriptionChange.ToString()
            };

            await repository.AddAsync(historyReservation);
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }

    }
}
