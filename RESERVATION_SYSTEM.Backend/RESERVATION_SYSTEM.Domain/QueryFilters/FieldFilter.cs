using RESERVATION_SYSTEM.Domain.Enums;

namespace RESERVATION_SYSTEM.Domain.QueryFilters
{
    public class FieldFilter
    {
        public string Field { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public TypeDateTime? TypeDateTime { get; set; }
        public DateTime? EndDate { get; set; }
        public TypeOrderBy? TypeOrderBy { get; set; }
    }
}
