using RESERVATION_SYSTEM.Domain.Entities.Base;
using RESERVATION_SYSTEM.Domain.Entities.reservation;

namespace RESERVATION_SYSTEM.Domain.Entities.customer
{
    public class Customer : DomainEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime DateRegistration { get; set; }

        public virtual IEnumerable<Reservation> Reservations { get; set; } = default!;
    }
}
