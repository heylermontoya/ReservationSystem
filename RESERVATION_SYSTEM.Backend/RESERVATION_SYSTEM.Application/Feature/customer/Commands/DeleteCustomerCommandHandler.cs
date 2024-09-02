using MediatR;
using RESERVATION_SYSTEM.Domain.Services.customer;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Commands
{
    public class DeleteCustomerCommandHandler : AsyncRequestHandler<DeleteCustomerCommand>
    {
        private readonly CustomerService service;

        public DeleteCustomerCommandHandler(CustomerService service)
        {
            this.service = service;
        }

        protected override async Task Handle(
            DeleteCustomerCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.DeleteCustomerAsync(
                command.Id
            );
        }
    }
}
