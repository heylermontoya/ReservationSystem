using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Commands
{
    public record CreateReservationCommand(
        [Required] Guid CustomerID,
        [Required] Guid ServiceID,
        [Required] DateTime StartDate,
        [Required] DateTime EndDate,
        [Required] int NumberPeople,
        [Required] float Total
    ) : IRequest;
}
