using Application.Medicines.Commands.Create;
using Application.PharmacyStores.Dtos;
using Application.PharmacyStores.Queries.GetPharmacyStoreById;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PharmacyStores.Queries.GetPharmacyStoreById
{
    public class GetPharmacyStoreByIdQueryHandler : IRequestHandler<GetPharmacyStoreByIdQuery, PharmacyStoreDto>
    {
        private readonly IUnitOfWork uow;

        public GetPharmacyStoreByIdQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<PharmacyStoreDto> Handle(
            GetPharmacyStoreByIdQuery request,
            CancellationToken cancellationToken)
        {
            var pharmacyStore = await uow.PharmacyStoreRepository.GetByIdAsync(request.Id);

            if (pharmacyStore == null)
                return null;

           return new PharmacyStoreDto
           {
               IdPharmacy = pharmacyStore.IdPharmacy,
               Name = pharmacyStore.Name
           };
        }
    }
}
