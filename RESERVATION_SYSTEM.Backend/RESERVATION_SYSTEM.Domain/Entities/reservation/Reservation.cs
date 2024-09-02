using RESERVATION_SYSTEM.Domain.Entities.Base;
using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Entities.service;

namespace RESERVATION_SYSTEM.Domain.Entities.reservation
{
    public class Reservation : DomainEntity
    {
        public Guid CustomerID { get; set; }
        public Guid? ServiceID { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string State { get; set; } = string.Empty;
        public int NumberPeople { get; set; }
        public float Total { get; set; }

        public virtual Service? Service { get; set; } = default!;
        public virtual Customer Customer { get; set; } = default!;
        public virtual IEnumerable<HistoryReservation> HistoryReservation { get; set; } = default!;

    }
}
