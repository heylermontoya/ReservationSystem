using Microsoft.EntityFrameworkCore;
using RESERVATION_SYSTEM.Domain.Entities.service;

namespace RESERVATION_SYSTEM.Infrastructure.EntitiesConfiguration
{
    internal static class ConfigureService
    {
        internal static void ConfigureModelService(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .HasKey(e => e.Id);
        }
    }
}
