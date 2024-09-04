using MediatR;
using RESERVATION_SYSTEM.Domain.Services.service;

namespace RESERVATION_SYSTEM.Application.Feature.service.Commands
{
    public class UpdateServiceCommandHandler : AsyncRequestHandler<UpdateServiceCommand>
    {
        private readonly ServiceService service;

        public UpdateServiceCommandHandler(ServiceService service)
        {
            this.service = service;
        }

        protected override async Task Handle(
            UpdateServiceCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.UpdateServiceAsync(
                command.Id,
                command.Name,
                command.Description,
                command.Price,
                command.Capacity
            );
        }
    }
}
