using RESERVATION_SYSTEM.Domain.Entities.historyReservation;
using RESERVATION_SYSTEM.Domain.Entities.reservation;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class HistoryReservationBuilder
    {
        private Guid _id;
        private Guid _reservationId;
        private DateTime _dateChange;
        private string _descriptionChange;
        private Reservation _reservation;

        public HistoryReservationBuilder()
        {
            _id = Guid.NewGuid();
            _reservationId = Guid.NewGuid();
            _dateChange = DateTime.Now;
            _descriptionChange = "Default Description";
            _reservation = new Reservation();
        }

        public HistoryReservation Build()
        {
            return new HistoryReservation
            {
                Id = _id,
                ReservationID = _reservationId,
                DateChange = _dateChange,
                DescriptionChange = _descriptionChange,
                Reservation = _reservation
            };
        }

        public HistoryReservationBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public HistoryReservationBuilder WithReservationId(Guid reservationId)
        {
            _reservationId = reservationId;
            return this;
        }

        public HistoryReservationBuilder WithDateChange(DateTime dateChange)
        {
            _dateChange = dateChange;
            return this;
        }

        public HistoryReservationBuilder WithDescriptionChange(string descriptionChange)
        {
            _descriptionChange = descriptionChange;
            return this;
        }

        public HistoryReservationBuilder WithReservation(Reservation reservation)
        {
            _reservation = reservation;
            return this;
        }
    }
}
