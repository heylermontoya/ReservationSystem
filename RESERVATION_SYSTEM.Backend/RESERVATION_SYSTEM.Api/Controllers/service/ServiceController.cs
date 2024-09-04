using MediatR;
using Microsoft.AspNetCore.Mvc;
using RESERVATION_SYSTEM.Application.Feature.service.Commands;
using RESERVATION_SYSTEM.Application.Feature.service.Queries;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Api.Controllers.service
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController
    {
        private readonly IMediator mediator;

        public ServiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("list")]
        public async Task<List<ServiceDto>> ObtainServiceAsync(
            IEnumerable<FieldFilter>? fieldFilter
        )
        {
            return await mediator.Send(
                new ObtainServiceQuery(fieldFilter)
            );
        }

        [HttpPost()]
        public async Task CreateServiceAsync(
            CreateServiceCommand command
        )
        {
            await mediator.Send(command);
        }

        [HttpPut()]
        public async Task UpdateServiceAsync(
            UpdateServiceCommand command
        )
        {
            await mediator.Send(command);
        }

        [HttpDelete()]
        public async Task DeleteServiceAsync(
            DeleteServiceCommand command
        )
        {
            await mediator.Send(command);
        }
    }
}
