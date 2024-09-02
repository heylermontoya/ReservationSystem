using MediatR;
using RESERVATION_SYSTEM.Domain.Services.service;

namespace RESERVATION_SYSTEM.Application.Feature.service.Commands
{
    public class CreateServiceCommandHandler : AsyncRequestHandler<CreateServiceCommand>
    {
        private readonly ServiceService service;

        public CreateServiceCommandHandler(ServiceService service)
        {
            this.service = service;
        }

        protected override async Task Handle(
            CreateServiceCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.CreateServiceAsync(
                command.Name,
                command.Description,
                command.Price,
                command.Capacity,
                command.Available
            );
        }
    }
}
