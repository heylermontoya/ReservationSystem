using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;
using RESERVATION_SYSTEM.Domain.Services.service;

namespace RESERVATION_SYSTEM.Domain.Services.reservation
{
    [DomainService]
    public class ReservationService
    {
        protected readonly IGenericRepository<Reservation> repository;
        protected readonly IQueryWrapper queryWrapper;
        protected readonly HistoryReservationService historyReservationService;
        protected readonly ServiceService serviceService;
        public ReservationService(
            IGenericRepository<Reservation> repository,
            IQueryWrapper queryWrapper,
            HistoryReservationService historyReservationService,
            ServiceService serviceService
        )
        {

            this.repository = repository;
            this.queryWrapper = queryWrapper;
            this.historyReservationService = historyReservationService;
            this.serviceService = serviceService;
        }

        public async Task<List<ReservationDto>> ObtainReservationAsync(IEnumerable<FieldFilter>? filters)
        {
            List<FieldFilter> listFilters = filters != null ? filters.ToList() : [];

            IEnumerable<ReservationDto> reservations =
                await queryWrapper
                    .QueryAsync<ReservationDto>(
                        ItemsMessageConstants.GetReservation
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(listFilters)
                    );

            return reservations.ToList();
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
            await serviceService.UpdateServiceNotAvailableAsync(serviceID, false);
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
            await serviceService.UpdateServiceNotAvailableAsync(reservation.ServiceID, true);

            reservation.ServiceID = serviceId;
            reservation.DateReservation = DateConverterHelper.ConvertDatetimeToLocalZone(DateTime.UtcNow);
            reservation.StartDate = startDate;
            reservation.EndDate = endDate;
            reservation.State = ReservationStatus.Modified.ToString();
            reservation.NumberPeople = numberPeople;
            reservation.Total = total;

            reservation = await repository.UpdateAsync(reservation);

            await historyReservationService.CreateHistoryReservationAsync(reservation.Id, ReservationStatus.Modified);
            await serviceService.UpdateServiceNotAvailableAsync(serviceId, false);
        }

        public async Task CancelReservationAsync(Guid id)
        {
            Reservation reservation = await ObtainReservationById(id);
            reservation.State = ReservationStatus.Canceled.ToString();

            reservation = await repository.UpdateAsync(reservation);

            await historyReservationService.CreateHistoryReservationAsync(reservation.Id, ReservationStatus.Canceled);
            await serviceService.UpdateServiceNotAvailableAsync(reservation.ServiceID, true);
        }

        public async Task LiberateServicesAsync(Guid idCustomer)
        {
            IEnumerable<Reservation> listReservation = await repository.GetAsync(
                reservation => reservation.CustomerID == idCustomer && 
                reservation.State != ReservationStatus.Canceled.ToString() &&
                reservation.ServiceID != null
            );

            foreach (var reservation in listReservation)
            {
                await serviceService.UpdateServiceNotAvailableAsync(reservation.ServiceID, true);
            }
        }
        private async Task<Reservation> ObtainReservationById(Guid id)
        {
            Reservation reservation = await repository.GetByIdAsync(id);
            return reservation;
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }
    }
}
