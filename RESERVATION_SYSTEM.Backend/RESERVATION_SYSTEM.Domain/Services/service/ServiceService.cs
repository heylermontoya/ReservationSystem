using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Ports;

namespace RESERVATION_SYSTEM.Domain.Services.service
{
    [DomainService]
    public class ServiceService
    {
        protected readonly IGenericRepository<Service> repository;
        protected readonly IQueryWrapper queryWrapper;
        public ServiceService(
            IGenericRepository<Service> repository,
            IQueryWrapper queryWrapper
        )
        {
            this.repository = repository;
            this.queryWrapper = queryWrapper;
        }

        public async Task CreateServiceAsync(
            string name,
            string description,
            float price,
            int capacity,
            bool available
        )
        {
            Service service = new()
            {
                Name = name,
                Description = description,
                Price = price,
                Capacity = capacity,
                Available = available
            };

            await repository.AddAsync(service);
        }

        public async Task UpdateServiceAsync(
            Guid id,
            string name,
            string description,
            float price,
            int capacity,
            bool available
        )
        {
            Service service = await ObtainServiceById(id);

            service.Name = name;
            service.Description = description;
            service.Price = price;
            service.Capacity = capacity;
            service.Available = available;

            await repository.UpdateAsync(service);
        }

        public async Task DeleteServiceAsync(Guid id)
        {
            Service service = await ObtainServiceById(id);

            //Se elimina deja en null todas las reservas asociadas a este servicio y el historial se deja intacto
            await repository.DeleteAsync(service);            
        }
        
        private async Task<Service> ObtainServiceById(Guid id)
        {
            Service service = await repository.GetByIdAsync(id);
            return service;
        }
    }
}
