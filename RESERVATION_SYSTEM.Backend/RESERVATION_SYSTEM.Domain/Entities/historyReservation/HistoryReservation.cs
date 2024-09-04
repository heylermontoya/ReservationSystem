using RESERVATION_SYSTEM.Domain.Entities.Base;
using RESERVATION_SYSTEM.Domain.Entities.reservation;

namespace RESERVATION_SYSTEM.Domain.Entities.historyReservation
{
    public class HistoryReservation : DomainEntity
    {
        public Guid ReservationID { get; set; }
        public DateTime DateChange { get; set; }
        public string DescriptionChange { get; set; } = string.Empty;

        public virtual Reservation Reservation { get; set; } = default!;
    }
}
