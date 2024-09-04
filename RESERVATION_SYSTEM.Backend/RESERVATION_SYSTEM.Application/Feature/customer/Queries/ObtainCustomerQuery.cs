using MediatR;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Queries
{
    public record ObtainCustomerQuery(IEnumerable<FieldFilter>? Filters) : IRequest<List<CustomerDto>>;
}
