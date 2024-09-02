using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;

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
    }
}
