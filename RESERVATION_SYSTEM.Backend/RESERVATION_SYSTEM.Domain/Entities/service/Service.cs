using RESERVATION_SYSTEM.Domain.Entities.Base;
using RESERVATION_SYSTEM.Domain.Entities.reservation;

namespace RESERVATION_SYSTEM.Domain.Entities.service
{
    public class Service : DomainEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }

        public virtual IEnumerable<Reservation> Reservations { get; set; } = default!;
    }
}
