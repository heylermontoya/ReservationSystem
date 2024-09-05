using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Entities.service;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class ServiceBuilder
    {
        private Guid _id;
        private string _name;
        private string _description;
        private float _price;
        private int _capacity;
        private bool _available;
        private IEnumerable<Reservation> _reservations;

        public ServiceBuilder()
        {
            _id = Guid.NewGuid();
            _name = "Default Service Name";
            _description = "Default Description";
            _price = 0.0f;
            _capacity = 1;
            _available = true;
            _reservations = new List<Reservation>();
        }

        public Service Build()
        {
            return new Service
            {
                Id = _id,
                Name = _name,
                Description = _description,
                Price = _price,
                Capacity = _capacity,
                Available = _available,
                Reservations = _reservations
            };
        }

        public ServiceBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ServiceBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ServiceBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ServiceBuilder WithPrice(float price)
        {
            _price = price;
            return this;
        }

        public ServiceBuilder WithCapacity(int capacity)
        {
            _capacity = capacity;
            return this;
        }

        public ServiceBuilder WithAvailable(bool available)
        {
            _available = available;
            return this;
        }

        public ServiceBuilder WithReservations(IEnumerable<Reservation> reservations)
        {
            _reservations = reservations;
            return this;
        }
    }
}
