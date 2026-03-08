using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bill.Commands.Update
{
    public class UpdateBillStatusCommandHandler : IRequestHandler<UpdateBillStatusCommand, bool>
    {
        private readonly IUnitOfWork uow;

        public UpdateBillStatusCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<bool> Handle(UpdateBillStatusCommand request, CancellationToken cancellationToken)
        {
            var bill = await uow.BillRepository.GetByIdAsync(request.IdBill);

            if (bill == null) return false;

            
            var validStatuses = new[] { "Odobren", "Storniran", "Obrada" };
            if (!validStatuses.Contains(request.NewStatus)) return false;

            bill.Status = request.NewStatus;

            await uow.SaveChangesAsync();
            return true;
        }
    }
}
