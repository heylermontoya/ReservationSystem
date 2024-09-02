using MediatR;
using RESERVATION_SYSTEM.Domain.Services.reservation;

namespace RESERVATION_SYSTEM.Application.Feature.reservation.Commands
{
    public class UpdateReservationCommandHandler : AsyncRequestHandler<UpdateReservationCommand>
    {
        private readonly ReservationService service;

        public UpdateReservationCommandHandler(ReservationService service)
        {
            this.service = service;
        }

        protected override async Task Handle(
            UpdateReservationCommand command,
            CancellationToken cancellationToken
        )
        {
            await service.UpdateReservationAsync(
               command.Id,
               command.ServiceId,
               command.StartDate,
               command.EndDate,
               command.NumberPeople,
               command.Total
            );
        }
    }
}