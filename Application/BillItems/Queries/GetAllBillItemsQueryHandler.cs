using Application.BillItems.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BillItems.Queries
{
    public class GetAllBillItemsQueryHandler
         : IRequestHandler<GetAllBillItemsQuery, List<BillItemDto>>
    {
        private readonly IUnitOfWork uow;

        public GetAllBillItemsQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<List<BillItemDto>> Handle(
            GetAllBillItemsQuery request,
            CancellationToken cancellationToken)
        {
            var items = await uow.BillItemRepository.GetAllAsync();

            return items.Select(x => new BillItemDto
            {
               
                Rb = x.Rb,
                Price = x.Price,
                Description = x.Description,
                IdMedicine = x.IdMedicine
            }).ToList();
        }
    }
}
