using Application.PharmacyStores.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PharmacyStores.Queries.GetAllPharmacyStores
{
    public class GetAllPharmacyStoresQueryHandler : IRequestHandler<GetAllPharmacyStoresQuery, List<PharmacyStoreDto>>
    {
        private readonly IUnitOfWork uow;

        public GetAllPharmacyStoresQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<List<PharmacyStoreDto>> Handle(GetAllPharmacyStoresQuery request, CancellationToken cancellationToken)
        {
            var stores = await uow.PharmacyStoreRepository.GetAllAsync();

            return stores.Select(s => new PharmacyStoreDto
            {
                IdPharmacy = s.IdPharmacy,
                Name = s.Name,
                Address = s.Address,
              
                Latitude = s.Latitude.ToString().Replace('.', ','),
                Longitude = s.Longitude.ToString().Replace('.', ',')
            }).ToList();
        }
    }
}
