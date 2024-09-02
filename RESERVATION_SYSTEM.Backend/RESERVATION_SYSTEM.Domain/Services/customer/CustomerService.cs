using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Entities.reservation;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.Services.historyReservation;

namespace RESERVATION_SYSTEM.Domain.Services.customer
{
    [DomainService]
    public class CustomerService
    {
        protected readonly IGenericRepository<Customer> repository;
        protected readonly IQueryWrapper queryWrapper;
        public CustomerService(
            IGenericRepository<Customer> repository,
            IQueryWrapper queryWrapper
        )
        {

            this.repository = repository;
            this.queryWrapper = queryWrapper;
        }

        public async Task CreateCustomerAsync(string name, string email, string phone)
        {
            Customer customer = new()
            {
                Name = name,
                Email = email,
                Phone = phone,
                DateRegistration = DateConverterHelper.ConvertDatetimeToLocalZone(DateTime.UtcNow)
            };

            await repository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(
            Guid id,
            string name, 
            string email,
            string phone
        )
        {
            Customer customer = await ObtainCustomerById(id);

            customer.Name = name;
            customer.Email = email;
            customer.Phone = phone;

            await repository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            Customer customer = await ObtainCustomerById(id);

            //Si se elimina un usuario, se eliminan todas las reservas asociadas y el historial asociado a esas reservas
            //Se realiza un borrado en cascada
            await repository.DeleteAsync(customer);
        }

        private async Task<Customer> ObtainCustomerById(Guid id)
        {
            Customer customer = await repository.GetByIdAsync(id);
            return customer;
        }
    }
}
