namespace RESERVATION_SYSTEM.Domain.DTOs
{
    public class HistoryReservationDto
    {
        public Guid ReservationId { get; set; }
        public DateTime DateChange { get; set; }
        public string DescriptionChange { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
    }
}
