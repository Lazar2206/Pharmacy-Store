using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bill.Commands.Create
{
    public class CreateBillCommandHandler
        : IRequestHandler<CreateBillCommand, int>
    {
        private readonly IUnitOfWork uow;

        public CreateBillCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<int> Handle(
            CreateBillCommand request,
            CancellationToken cancellationToken)
        {
            var bill = new Domain.Bill
            {
                Date = request.Date,
                TotalPrice = request.TotalPrice,
                IdPharmacy = request.IdPharmacy,
                IdPatient = request.IdPatient,
                Status = "Obrada"
            };

            uow.BillRepository.Add(bill);
            await uow.SaveChangesAsync();

            return bill.IdBill;
        }
    }
}
