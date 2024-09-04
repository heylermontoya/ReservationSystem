using System.ComponentModel;

namespace RESERVATION_SYSTEM.Domain.Enums
{
    public enum ItemsMessageConstants
    {        
        [Description("GetCustomers")]
        GetCustomers,       
        [Description("GetServices")]
        GetServices,
        [Description("GetReservation")]
        GetReservation,
        [Description("GetHistoryReservation")]
        GetHistoryReservation
    }
}
