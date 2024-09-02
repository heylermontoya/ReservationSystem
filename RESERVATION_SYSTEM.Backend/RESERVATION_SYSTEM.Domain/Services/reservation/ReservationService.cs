using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;

namespace RESERVATION_SYSTEM.Domain.Services.reservation
{
    [DomainService]
    public class ReservationService
    {
        protected readonly IGenericRepository<Reservation> repository;
        protected readonly IQueryWrapper queryWrapper;
        protected readonly HistoryReservationService historyReservationService;
        public ReservationService(
            IGenericRepository<Reservation> repository,
            IQueryWrapper queryWrapper,
            HistoryReservationService historyReservationService
        )
        {

            this.repository = repository;
            this.queryWrapper = queryWrapper;
            this.historyReservationService = historyReservationService;
        }

        public async Task<List<ReservationDto>> ObtainReservationAsync(Filter filter)
        {
            return [];
        }

        public async Task CreateReservationAsync(
            Guid customerID,
            Guid serviceID,
            DateTime startDate,
            DateTime endDate,
            int numberPeople,
            float total
        )
        {
            Reservation reservation = new()
            {
                CustomerID = customerID,
                ServiceID = serviceID,
                DateReservation = DateConverterHelper.ConvertDatetimeToLocalZone(DateTime.UtcNow),
                StartDate = startDate,
                EndDate = endDate,
                State = ReservationStatus.Confirmed.ToString(),
                NumberPeople = numberPeople,
                Total = total                
            };

            reservation = await repository.AddAsync(reservation);

            await historyReservationService.CreateHistoryReservationAsync(reservation.Id, ReservationStatus.Confirmed);
        }
        
        public async Task UpdateReservationAsync(
            Guid id,
            Guid serviceId,
            DateTime startDate,
            DateTime endDate,
            int numberPeople,
            float total
        )
        {
            Reservation reservation = await ObtainReservationById(id);

            reservation.ServiceID = serviceId;
            reservation.DateReservation = DateConverterHelper.ConvertDatetimeToLocalZone(DateTime.UtcNow);
            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            reservation.State = ReservationStatus.Modified.ToString();
            reservation.NumberPeople = numberPeople;
            reservation.Total = total;            

            reservation = await repository.UpdateAsync(reservation);

            await historyReservationService.CreateHistoryReservationAsync(reservation.Id, ReservationStatus.Modified);
        }

        public async Task CancelReservationAsync(Guid id)
        {
            Reservation reservation = await ObtainReservationById(id);
            reservation.State = ReservationStatus.Canceled.ToString();

            reservation = await repository.UpdateAsync(reservation);

            await historyReservationService.CreateHistoryReservationAsync(reservation.Id, ReservationStatus.Canceled);
        }

        private async Task<Reservation> ObtainReservationById(Guid id)
        {
            Reservation reservation = await repository.GetByIdAsync(id);
            return reservation;
        }
    }
}
