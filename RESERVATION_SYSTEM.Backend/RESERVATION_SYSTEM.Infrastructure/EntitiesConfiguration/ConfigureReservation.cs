using Microsoft.EntityFrameworkCore;
using RESERVATION_SYSTEM.Domain.Entities.reservation;

namespace RESERVATION_SYSTEM.Infrastructure.EntitiesConfiguration
{
    internal static class ConfigureReservation
    {
        internal static void ConfigureModelReservation(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Service)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.ServiceID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
