using MediatR;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;

namespace RESERVATION_SYSTEM.Application.Feature.historyReservation.Queries
{
    public class ObtainHistoryReservationQueryHandler :
        IRequestHandler<ObtainHistoryReservationQuery, List<HistoryReservationDto>>
    {
        private readonly HistoryReservationService service;

        public ObtainHistoryReservationQueryHandler(HistoryReservationService service)
        {
            this.service = service;
        }

        public async Task<List<HistoryReservationDto>> Handle(ObtainHistoryReservationQuery query, CancellationToken cancellationToken)
        {
            List<HistoryReservationDto> historyReservationDto = await
                service.ObtainHistoryReservationDtoAsync(query.Filters);

            return historyReservationDto;
        }
    }
}
