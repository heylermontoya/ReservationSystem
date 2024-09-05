using RESERVATION_SYSTEM.Domain.DTOs;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class CustomerDtoBuilder
    {
        private Guid _id;
        private string _name;
        private string _email;
        private string _phone;
        private DateTime _dateRegistration;

        public CustomerDtoBuilder()
        {
            _id = Guid.NewGuid();
            _name = "Default Name";
            _email = "default@example.com";
            _phone = "000-000-0000";
            _dateRegistration = DateTime.Now;
        }

        public CustomerDto Build()
        {
            return new CustomerDto
            {
                Id = _id,
                Name = _name,
                Email = _email,
                Phone = _phone,
                DateRegistration = _dateRegistration
            };
        }

        public CustomerDtoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public CustomerDtoBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CustomerDtoBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public CustomerDtoBuilder WithPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        public CustomerDtoBuilder WithDateRegistration(DateTime dateRegistration)
        {
            _dateRegistration = dateRegistration;
            return this;
        }
    }
}
