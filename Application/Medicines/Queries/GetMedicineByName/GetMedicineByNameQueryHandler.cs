using Application.Medicines.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Medicines.Queries.GetMedicineByName
{
    public class GetMedicineByNameQueryHandler : IRequestHandler<GetMedicineByNameQuery, IEnumerable<MedicineDto>>
    {
        private readonly IUnitOfWork _uow;
        public GetMedicineByNameQueryHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<IEnumerable<MedicineDto>> Handle(GetMedicineByNameQuery request, CancellationToken cancellationToken)
        {
          
            var medicines = await _uow.MedicineRepository.GetByNameAsync(request.Name);

            
            return medicines.Select(m => new MedicineDto
            {
                IdMedicine = m.IdMedicine,
                Name = m.Name,
                Price = m.Price
            });
        }
    }
}
