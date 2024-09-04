using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Entities.service;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;

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

        public async Task<List<ServiceDto>> ObtainServiceAsync(IEnumerable<FieldFilter>? filters)
        {

            List<FieldFilter> listFilters = filters != null ? filters.ToList() : [];

            IEnumerable<ServiceDto> servicess =
                await queryWrapper
                    .QueryAsync<ServiceDto>(
                        ItemsMessageConstants.GetServices
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(listFilters)
                    );

            return servicess.ToList();
        }

        public async Task CreateServiceAsync(
            string name,
            string description,
            float price,
            int capacity
        )
        {
            Service service = new()
            {
                Name = name,
                Description = description,
                Price = price,
                Capacity = capacity,
                Available = true
            };

            IEnumerable<Service> listService = await repository.GetAsync(
                service => service.Name == name
            );

            if (listService.Any())
            {
                throw new AppException(MessagesExceptions.NameServiceNotValid);
            }

            await repository.AddAsync(service);
        }

        public async Task UpdateServiceAsync(
            Guid id,
            string name,
            string description,
            float price,
            int capacity
        )
        {
            Service service = await ObtainServiceById(id);

            IEnumerable<Service> listService = await repository.GetAsync(
                service => service.Name == name
            );

            if (listService.Any() && name != service.Name)
            {
                throw new AppException(MessagesExceptions.NameServiceNotValid);
            }

            service.Name = name;
            service.Description = description;
            service.Price = price;
            service.Capacity = capacity;

            await repository.UpdateAsync(service);
        }

        public async Task UpdateServiceNotAvailableAsync(Guid? id, bool available)
        {
            Service service = await ObtainServiceById(id);

            service.Available = available;

            await repository.UpdateAsync(service);
        }

        public async Task DeleteServiceAsync(Guid id)
        {
            Service service = await ObtainServiceById(id);

            await repository.DeleteAsync(service);
        }

        private async Task<Service> ObtainServiceById(Guid? id)
        {
            Service service = await repository.GetByIdAsync(id!);
            return service;
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }
    }
}
