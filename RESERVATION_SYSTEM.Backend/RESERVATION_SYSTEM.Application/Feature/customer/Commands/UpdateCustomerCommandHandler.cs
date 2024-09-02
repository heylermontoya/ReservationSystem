using MediatR;
using RESERVATION_SYSTEM.Domain.Services.customer;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Commands
{
    public class UpdateCustomerCommandHandler : AsyncRequestHandler<UpdateCustomerCommand>
    {
        private readonly CustomerService service;

        public UpdateCustomerCommandHandler(CustomerService service)
        {
            this.service = service;
        }

        protected override async Task Handle(
            UpdateCustomerCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.UpdateCustomerAsync(
                command.Id,
                command.Name,
                command.Email,
                command.Phone
            );
        }
    }
}
