using RESERVATION_SYSTEM.Domain.DTOs;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class HistoryReservationDtoBuilder
    {
        private Guid _reservationId;
        private DateTime _dateChange;
        private string _descriptionChange;
        private string _serviceName;
        private string _customerName;

        public HistoryReservationDtoBuilder()
        {
            _reservationId = Guid.NewGuid();
            _dateChange = DateTime.Now;
            _descriptionChange = "Default Description";
            _serviceName = "Default Service";
            _customerName = "Default Customer";
        }

        public HistoryReservationDto Build()
        {
            return new HistoryReservationDto
            {
                ReservationId = _reservationId,
                DateChange = _dateChange,
                DescriptionChange = _descriptionChange,
                ServiceName = _serviceName,
                CustomerName = _customerName
            };
        }

        public HistoryReservationDtoBuilder WithReservationId(Guid reservationId)
        {
            _reservationId = reservationId;
            return this;
        }

        public HistoryReservationDtoBuilder WithDateChange(DateTime dateChange)
        {
            _dateChange = dateChange;
            return this;
        }

        public HistoryReservationDtoBuilder WithDescriptionChange(string descriptionChange)
        {
            _descriptionChange = descriptionChange;
            return this;
        }

        public HistoryReservationDtoBuilder WithServiceName(string serviceName)
        {
            _serviceName = serviceName;
            return this;
        }

        public HistoryReservationDtoBuilder WithCustomerName(string customerName)
        {
            _customerName = customerName;
            return this;
        }
    }
}
