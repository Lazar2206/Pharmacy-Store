using Application.BillItems.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bill.Commands.Queries
{
    public class GetBillByIdQueryHandler
         : IRequestHandler<GetBillByIdQuery, BillDto>
    {
        private readonly IUnitOfWork uow;

        public GetBillByIdQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<BillDto> Handle(
            GetBillByIdQuery request,
            CancellationToken cancellationToken)
        {
            var bill = await uow.BillRepository.GetByIdWithItemsAsync(request.Id);

            if (bill == null)
                return null;

            return new BillDto
            {
                IdBill = bill.IdBill,
                Date = bill.Date,
                TotalPrice = bill.TotalPrice,
                IdPharmacy = bill.IdPharmacy,
                IdPatient = bill.IdPatient,
                BillItems = bill.BillItems.Select(bi => new BillItemDto
                {
                    Rb = bi.Rb,
                    Price = bi.Price,
                    Description = bi.Description,
                    IdMedicine = bi.IdMedicine
                }).ToList()
            };
        }
    }
}
