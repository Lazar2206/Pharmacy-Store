using Domain;
using MediatR;
using Infrastructure.UnitOfWork;

using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Repositories.Interfaces;

namespace Application.PharmacyStores.Commands.Create
{
    public class CreatePharmacyStoreCommandHandler
         : IRequestHandler<CreatePharmacyStoreCommand, int>
    {
        private readonly IUnitOfWork uow;
        private readonly IMapService _mapService; 

        public CreatePharmacyStoreCommandHandler(IUnitOfWork uow, IMapService mapService)
        {
            this.uow = uow;
            this._mapService = mapService; 
        }

        public async Task<int> Handle(
            CreatePharmacyStoreCommand request,
            CancellationToken cancellationToken)
        {
            
            var coordinates = await _mapService.GetCoordinatesAsync(request.Address);

            var pharmacyStore = new Domain.PharmacyStore
            {
                Name = request.Name,
                Address = request.Address, 
                Latitude = coordinates?.Lat ?? 0, 
                Longitude = coordinates?.Lng ?? 0
            };

            uow.PharmacyStoreRepository.Add(pharmacyStore);
            await uow.SaveChangesAsync();

            return pharmacyStore.IdPharmacy;
        }
    }
}