using Application.PharmacyStores.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PharmacyStores.Queries.GetPharmacyStoreByName
{
    public class GetPharmacyStoreByNameQueryHandler : IRequestHandler<GetPharmacyStoreByNameQuery, IEnumerable<PharmacyStoreDto>>
    {
        private readonly IUnitOfWork _uow;
        public GetPharmacyStoreByNameQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<PharmacyStoreDto>> Handle(GetPharmacyStoreByNameQuery request, CancellationToken cancellationToken)
        {
            var stores = await _uow.PharmacyStoreRepository.GetByNameAsync(request.Name);

            return stores.Select(ps => new PharmacyStoreDto
            {
                IdPharmacy = ps.IdPharmacy,
                Name = ps.Name,
                Address = ps.Address,
                Latitude = ps.Latitude.ToString(), 
                Longitude = ps.Longitude.ToString()
            });
        }
    }
}
