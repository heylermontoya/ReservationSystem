using MediatR;
using RESERVATION_SYSTEM.Domain.DTOs;
using RESERVATION_SYSTEM.Domain.Services.service;

namespace RESERVATION_SYSTEM.Application.Feature.service.Queries
{
    public class ObtainServiceQueryHandler : IRequestHandler<ObtainServiceQuery, List<ServiceDto>>
    {
        private readonly ServiceService service;

        public ObtainServiceQueryHandler(ServiceService service)
        {
            this.service = service;
        }

        public async Task<List<ServiceDto>> Handle(ObtainServiceQuery query, CancellationToken cancellationToken)
        {
            var listServices= await service.ObtainServiceAsync(query.filters);
            return listServices;
        }
    }
}
