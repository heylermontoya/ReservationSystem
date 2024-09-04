using MediatR;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Application.Feature.service.Queries
{
    public record ObtainServiceQuery(IEnumerable<FieldFilter>? filters) : IRequest<List<ServiceDto>>;
}
