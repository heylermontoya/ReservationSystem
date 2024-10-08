﻿using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Entities.customer;
using RESERVATION_SYSTEM.Domain.Enums;
using RESERVATION_SYSTEM.Domain.Exceptions;
using RESERVATION_SYSTEM.Domain.Helpers;
using RESERVATION_SYSTEM.Domain.Ports;
using RESERVATION_SYSTEM.Domain.QueryFilters;
using RESERVATION_SYSTEM.Domain.Services.reservation;

namespace RESERVATION_SYSTEM.Domain.Services.customer
{
    [DomainService]
    public class CustomerService
    {
        protected readonly IGenericRepository<Customer> repository;
        protected readonly IQueryWrapper queryWrapper;
        private readonly ReservationService reservationService;
        public CustomerService(
            IGenericRepository<Customer> repository,
            IQueryWrapper queryWrapper,
            ReservationService reservationService
        )
        {

            this.repository = repository;
            this.queryWrapper = queryWrapper;
            this.reservationService = reservationService;
        }

        public async Task<List<CustomerDto>> ObtainCustomerAsync(IEnumerable<FieldFilter>? filters)
        {

            List<FieldFilter> listFilters = filters != null ? filters.ToList() : [];

            IEnumerable<CustomerDto> Customers =
                await queryWrapper
                    .QueryAsync<CustomerDto>(
                        ItemsMessageConstants.GetCustomers
                            .GetDescription(),
                        new
                        { },
                        BuildQueryArgs(listFilters)
                    );

            return Customers.ToList();
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

            IEnumerable<Customer> listCustomer = await repository.GetAsync(
                customer => customer.Name == name
            );

            if (listCustomer.Any())
            {
                throw new AppException(MessagesExceptions.NameCustomerNotValid);
            }

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

            IEnumerable<Customer> listCustomer = await repository.GetAsync(
                customer => customer.Name == name
            );

            if (listCustomer.Any() && name != customer.Name)
            {
                throw new AppException(MessagesExceptions.NameCustomerNotValid);
            }

            customer.Name = name;
            customer.Email = email;
            customer.Phone = phone;

            await repository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            Customer customer = await ObtainCustomerById(id);

            await reservationService.LiberateServicesAsync(id);

            await repository.DeleteAsync(customer);
        }

        private async Task<Customer> ObtainCustomerById(Guid id)
        {
            Customer customer = await repository.GetByIdAsync(id);
            return customer;
        }

        private static object[] BuildQueryArgs(IEnumerable<FieldFilter> listFilters)
        {
            string conditionQuery = FieldFilterHelper.BuildQuery(addWhereClause: true, listFilters);
            return [conditionQuery];
        }
    }
}
