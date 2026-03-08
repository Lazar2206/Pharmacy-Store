using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Medicines.Commands.Delete
{
    public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand, bool>
    {
        private readonly IUnitOfWork uow;

        public DeleteMedicineCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<bool> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
        {
            
            var medicine = await uow.MedicineRepository.GetByIdAsync(request.Id);

            if (medicine == null)
                return false;

            uow.MedicineRepository.Remove(medicine);
            await uow.SaveChangesAsync();

            return true;
        }
    }
}
