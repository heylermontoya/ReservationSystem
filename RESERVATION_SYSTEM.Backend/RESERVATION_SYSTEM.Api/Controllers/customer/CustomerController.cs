using MediatR;
using Microsoft.AspNetCore.Mvc;
using RESERVATION_SYSTEM.Application.Feature.customer.Commands;
using RESERVATION_SYSTEM.Application.Feature.customer.Queries;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Api.Controllers.customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("list")]
        public async Task<List<CustomerDto>> ObtainCustomerAsync(
            IEnumerable<FieldFilter>? fieldFilter
        )
        {
            return await mediator.Send(
                new ObtainCustomerQuery(fieldFilter)
            );
        }

        [HttpPost()]
        public async Task CreateCustomerAsync(
            CreateCustomerCommand command
        )
        {
            await mediator.Send(command);
        }

        [HttpPut()]
        public async Task UpdateCustomerAsync(
            UpdateCustomerCommand command
        )
        {
            await mediator.Send(command);
        }

        [HttpDelete()]
        public async Task DeleteCustomerAsync(
            DeleteCustomerCommand command
        )
        {
            await mediator.Send(command);
        }
    }
}
