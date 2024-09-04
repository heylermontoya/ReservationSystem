using MediatR;
using RESERVATION_SYSTEM.Application.Feature.customer.Queries;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Services.customer;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESERVATION_SYSTEM.Application.Feature.historyReservation.Queries
{
    public class ObtainHistoryReservationQueryHandler : IRequestHandler<ObtainHistoryReservationQuery, List<HistoryReservationDto>>
    {
        private readonly HistoryReservationService service;

        public ObtainHistoryReservationQueryHandler(HistoryReservationService service)
        {
            this.service = service;
        }

        public async Task<List<HistoryReservationDto>> Handle(ObtainHistoryReservationQuery query, CancellationToken cancellationToken)
        {
            List<HistoryReservationDto> historyReservationDto = await service.ObtainHistoryReservationDtoAsync(query.filters);
            return historyReservationDto;
        }
    }
}
