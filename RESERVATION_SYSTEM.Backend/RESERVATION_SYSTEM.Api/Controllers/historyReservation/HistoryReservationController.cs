using MediatR;
using Microsoft.AspNetCore.Mvc;
using RESERVATION_SYSTEM.Application.Feature.historyReservation.Queries;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Api.Controllers.historyReservation
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryReservationController
    {
        private readonly IMediator mediator;

        public HistoryReservationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("list")]
        public async Task<List<HistoryReservationDto>> ObtainHistoryReservationAsync(
            IEnumerable<FieldFilter>? fieldFilter
        )
        {
            return await mediator.Send(
                new ObtainHistoryReservationQuery(fieldFilter)
            );
        }
    }
}
