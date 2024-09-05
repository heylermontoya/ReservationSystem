using RESERVATION_SYSTEM.Domain.DTOs;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class ReservationDtoBuilder
    {
        private Guid _id;
        private Guid _customerId;
        private string _customerName;
        private Guid? _serviceId;
        private string? _serviceName;
        private DateTime _dateReservation;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _state;
        private int _numberPeople;
        private float _total;

        public ReservationDtoBuilder()
        {
            _id = Guid.NewGuid();
            _customerId = Guid.NewGuid();
            _customerName = "Default Customer";
            _serviceId = null;
            _serviceName = null;
            _dateReservation = DateTime.Now;
            _startDate = DateTime.Now;
            _endDate = DateTime.Now.AddDays(1);
            _state = "Pending";
            _numberPeople = 1;
            _total = 0.0f;
        }

        public ReservationDto Build()
        {
            return new ReservationDto
            {
                Id = _id,
                CustomerID = _customerId,
                CustomerName = _customerName,
                ServiceID = _serviceId,
                ServiceName = _serviceName,
                DateReservation = _dateReservation,
                StartDate = _startDate,
                EndDate = _endDate,
                State = _state,
                NumberPeople = _numberPeople,
                Total = _total
            };
        }

        public ReservationDtoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ReservationDtoBuilder WithCustomerId(Guid customerId)
        {
            _customerId = customerId;
            return this;
        }

        public ReservationDtoBuilder WithCustomerName(string customerName)
        {
            _customerName = customerName;
            return this;
        }

        public ReservationDtoBuilder WithServiceId(Guid? serviceId)
        {
            _serviceId = serviceId;
            return this;
        }

        public ReservationDtoBuilder WithServiceName(string? serviceName)
        {
            _serviceName = serviceName;
            return this;
        }

        public ReservationDtoBuilder WithDateReservation(DateTime dateReservation)
        {
            _dateReservation = dateReservation;
            return this;
        }

        public ReservationDtoBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public ReservationDtoBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public ReservationDtoBuilder WithState(string state)
        {
            _state = state;
            return this;
        }

        public ReservationDtoBuilder WithNumberPeople(int numberPeople)
        {
            _numberPeople = numberPeople;
            return this;
        }

        public ReservationDtoBuilder WithTotal(float total)
        {
            _total = total;
            return this;
        }
    }
}
