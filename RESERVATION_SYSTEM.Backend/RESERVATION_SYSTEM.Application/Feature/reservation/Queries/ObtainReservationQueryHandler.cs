using MediatR;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Services.reservation;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Queries
{
    public class ObtainReservationQueryHandler
        : IRequestHandler<ObtainReservationQuery, List<ReservationDto>>
    {
        private readonly ReservationService service;

        public ObtainReservationQueryHandler(ReservationService service)
        {
            this.service = service;
        }

        public async Task<List<ReservationDto>>
            Handle(ObtainReservationQuery query, CancellationToken cancellationToken)
        {
            List<ReservationDto> customer = await service.ObtainReservationAsync(query.Filters);
            return customer;
        }
    }
}
