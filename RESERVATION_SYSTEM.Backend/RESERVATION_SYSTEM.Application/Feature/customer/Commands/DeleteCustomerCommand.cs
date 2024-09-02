using MediatR;
using System.ComponentModel.DataAnnotations;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Commands
{
    public record DeleteCustomerCommand(
    [Required] Guid Id
) : IRequest;
}
