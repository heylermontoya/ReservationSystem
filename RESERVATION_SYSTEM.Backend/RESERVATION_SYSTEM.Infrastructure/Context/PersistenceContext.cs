using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RESERVATION_SYSTEM.Infrastructure.EntitiesConfiguration;

namespace RESERVATION_SYSTEM.Infrastructure.Context
{
    public class PersistenceContext : DbContext
    {
        private readonly IConfiguration _config;

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync().ConfigureAwait(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }

            modelBuilder.HasDefaultSchema(_config.GetValue<string>("SchemaName"));

            #region Models
            modelBuilder.ConfigureModelCustomer();
            modelBuilder.ConfigureModelService();
            modelBuilder.ConfigureModelReservation();
            modelBuilder.ConfigureModelHistoryReservation();
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
