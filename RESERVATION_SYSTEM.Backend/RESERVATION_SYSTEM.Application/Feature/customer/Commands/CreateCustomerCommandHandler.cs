using MediatR;
using RESERVATION_SYSTEM.Domain.Services.customer;

namespace RESERVATION_SYSTEM.Application.Feature.customer.Commands
{
    public class CreateCustomerCommandHandler : AsyncRequestHandler<CreateCustomerCommand>
    {
        private readonly CustomerService service;

        public CreateCustomerCommandHandler(CustomerService service)
        {
            this.service = service;
        }

        protected override async Task Handle(
            CreateCustomerCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.CreateCustomerAsync(
                command.Name,
                command.Email,
                command.Phone
            );
        }
    }
}
