using MediatR;
using RESERVATION_SYSTEM.Domain.Services.service;

namespace RESERVATION_SYSTEM.Application.Feature.service.Commands
{
    public class DeleteServiceCommandHandler : AsyncRequestHandler<DeleteServiceCommand>
    {
        private readonly ServiceService service;

        public DeleteServiceCommandHandler(ServiceService service)
        {
            this.service = service;
        }

        protected override async Task Handle(
            DeleteServiceCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.DeleteServiceAsync(
                command.Id
            );
        }
    }
}
