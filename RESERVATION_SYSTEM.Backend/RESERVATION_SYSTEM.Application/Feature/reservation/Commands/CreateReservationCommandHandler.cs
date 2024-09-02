using MediatR;
using RESERVATION_SYSTEM.Domain.Services.reservation;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Commands
{
    public class CreateReservationCommandHandler : AsyncRequestHandler<CreateReservationCommand>
    {
        private readonly ReservationService service;

        public CreateReservationCommandHandler(ReservationService service)
        {
            this.service = service;
        }

        protected override async Task Handle(
            CreateReservationCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.CreateReservationAsync(
               command.CustomerID,
               command.ServiceID,
               command.StartDate,
               command.EndDate,
               command.NumberPeople,
               command.Total
            );
        }
    }
}
