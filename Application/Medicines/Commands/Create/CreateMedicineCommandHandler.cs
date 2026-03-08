
using Application.Medicines.Queries.GetMedicineById;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Medicines.Commands.Create
{
    public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, int>
    {
        private readonly IUnitOfWork uow;

        public CreateMedicineCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<int> Handle(
            CreateMedicineCommand request,
            CancellationToken cancellationToken)
        {
            var medicine = new Domain.Medicine
            {
                
                Name = request.Name,
                Price = request.Price,
                Tags = request.Tags
            };

            uow.MedicineRepository.Add(medicine);
            await uow.SaveChangesAsync();

            return medicine.IdMedicine;
        }
    }
}
