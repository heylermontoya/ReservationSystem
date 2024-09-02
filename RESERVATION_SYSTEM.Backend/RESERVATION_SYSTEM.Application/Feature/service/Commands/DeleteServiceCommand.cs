using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RESERVATION_SYSTEM.Application.Feature.service.Commands
{
    public record DeleteServiceCommand(
        [Required] Guid Id
    ) : IRequest;
}
