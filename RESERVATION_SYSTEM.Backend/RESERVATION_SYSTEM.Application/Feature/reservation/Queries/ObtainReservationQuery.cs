using MediatR;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.QueryFilters;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Queries
{
    public record ObtainReservationQuery(Filter filter) : IRequest<List<ReservationDto>>;

}
