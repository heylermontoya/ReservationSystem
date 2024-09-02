using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Commands
{
    public record CreateCustomerCommand(
        [Required] string Name,
        [Required] string Email,
        [Required] string Phone
    ) : IRequest;
}
