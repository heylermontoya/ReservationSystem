using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Entities.reservation;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class CustomerBuilder
    {
        private Guid _id;
        private string _name;
        private string _email;
        private string _phone;
        private DateTime _dateRegistration;
        private IEnumerable<Reservation> _reservations;

        public CustomerBuilder()
        {
            _id = Guid.NewGuid();
            _name = "Default Name";
            _email = "default@example.com";
            _phone = "000-000-0000";
            _dateRegistration = DateTime.Now;
            _reservations = [];
        }

        public Customer Build()
        {
            return new Customer
            {
                Id = _id,
                Name = _name,
                Email = _email,
                Phone = _phone,
                DateRegistration = _dateRegistration,
                Reservations = _reservations
            };
        }

        public CustomerBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public CustomerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CustomerBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public CustomerBuilder WithPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        public CustomerBuilder WithDateRegistration(DateTime dateRegistration)
        {
            _dateRegistration = dateRegistration;
            return this;
        }

        public CustomerBuilder WithReservations(IEnumerable<Reservation> reservations)
        {
            _reservations = reservations;
            return this;
        }
    }
}
