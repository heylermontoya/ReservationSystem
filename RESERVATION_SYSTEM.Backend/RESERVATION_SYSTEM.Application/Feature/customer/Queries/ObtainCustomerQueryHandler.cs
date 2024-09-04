using MediatR;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Services.customer;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Queries
{
    public class ObtainCustomerQueryHandler
        : IRequestHandler<ObtainCustomerQuery, List<CustomerDto>>
    {
        private readonly CustomerService service;

        public ObtainCustomerQueryHandler(CustomerService service)
        {
            this.service = service;
        }

        public async Task<List<CustomerDto>> Handle(ObtainCustomerQuery query, CancellationToken cancellationToken)
        {
            List<CustomerDto> customer = await service.ObtainCustomerAsync(query.filters);
            return customer;
        }
    }
}
