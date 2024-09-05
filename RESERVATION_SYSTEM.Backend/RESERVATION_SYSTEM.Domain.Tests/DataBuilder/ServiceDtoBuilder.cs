using RESERVATION_SYSTEM.Domain.DTOs;

namespace RESERVATION_SYSTEM.Domain.Tests.DataBuilder
{
    public class ServiceDtoBuilder
    {
        private Guid _id;
        private string _name;
        private string _description;
        private float _price;
        private int _capacity;
        private bool _available;

        public ServiceDtoBuilder()
        {
            _id = Guid.NewGuid();
            _name = "Default Service Name";
            _description = "Default Description";
            _price = 0.0f;
            _capacity = 1;
            _available = true;
        }

        public ServiceDto Build()
        {
            return new ServiceDto
            {
                Id = _id,
                Name = _name,
                Description = _description,
                Price = _price,
                Capacity = _capacity,
                Available = _available
            };
        }

        public ServiceDtoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ServiceDtoBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ServiceDtoBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ServiceDtoBuilder WithPrice(float price)
        {
            _price = price;
            return this;
        }

        public ServiceDtoBuilder WithCapacity(int capacity)
        {
            _capacity = capacity;
            return this;
        }

        public ServiceDtoBuilder WithAvailable(bool available)
        {
            _available = available;
            return this;
        }
    }
}
