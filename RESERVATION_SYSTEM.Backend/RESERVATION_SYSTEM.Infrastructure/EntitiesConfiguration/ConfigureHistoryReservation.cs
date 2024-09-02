using Microsoft.EntityFrameworkCore;
using RESERVATION_SYSTEM.Domain.Entities.historyReservation;

namespace RESERVATION_SYSTEM.Infrastructure.EntitiesConfiguration
{
    internal static class ConfigureHistoryReservation
    {
        internal static void ConfigureModelHistoryReservation(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoryReservation>()
                .HasOne(hr => hr.Reservation)
                .WithMany(r => r.HistoryReservation)
                .HasForeignKey(hr => hr.ReservationID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
