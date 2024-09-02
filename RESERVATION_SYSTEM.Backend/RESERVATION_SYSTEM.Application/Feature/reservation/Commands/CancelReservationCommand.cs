using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Commands
{
    public record CancelReservationCommand(
        [Required] Guid Id
    ) : IRequest;
}
