using Microsoft.EntityFrameworkCore;
using RESERVATION_SYSTEM.Domain.Entities.customer;

namespace RESERVATION_SYSTEM.Infrastructure.EntitiesConfiguration
{
    internal static class ConfigureCustomer
    {
        internal static void ConfigureModelCustomer(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(e => e.Id);
        }
    }
}
