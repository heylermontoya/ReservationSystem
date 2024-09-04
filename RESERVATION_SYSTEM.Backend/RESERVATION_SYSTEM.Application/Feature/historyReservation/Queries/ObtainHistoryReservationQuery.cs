using MediatR;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Application.Feature.historyReservation.Queries
{
    public record ObtainHistoryReservationQuery(IEnumerable<FieldFilter>? Filters) :
        IRequest<List<HistoryReservationDto>>;
}
