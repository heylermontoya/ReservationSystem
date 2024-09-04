using MediatR;
using Microsoft.AspNetCore.Mvc;
using RESERVATION_SYSTEM.Application.Feature.reservation.Commands;
using RESERVATION_SYSTEM.Application.Feature.reservation.Queries;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Api.Controllers.reservation
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController
    {
        private readonly IMediator mediator;

        public ReservationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("list")]
        public async Task<List<ReservationDto>> ObtainReservationAsync(
            IEnumerable<FieldFilter>? fieldFilter
        )
        {
            return await mediator.Send(
                new ObtainReservationQuery(fieldFilter)
            );
        }

        [HttpPost()]
        public async Task CreateReservationAsync(
            CreateReservationCommand command
        )
        {
            await mediator.Send(command);
        }
        
        [HttpPut()]
        public async Task UpdateReservationAsync(
            UpdateReservationCommand command
        )
        {
            await mediator.Send(command);
        }
        
        [HttpPut("cancel")]
        public async Task UpdateReservationAsync(
            CancelReservationCommand command
        )
        {
            await mediator.Send(command);
        }
    }
}
