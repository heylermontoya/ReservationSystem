using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Commands
{
    public record UpdateCustomerCommand(
        [Required] Guid Id,
        [Required] string Name,
        [Required] string Email,
        [Required] string Phone
    ) : IRequest;
}
