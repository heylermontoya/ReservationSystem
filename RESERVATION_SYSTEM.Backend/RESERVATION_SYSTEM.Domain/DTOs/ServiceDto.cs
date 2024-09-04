namespace RESERVATION_SYSTEM.Domain.DTOs
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
    }
}
