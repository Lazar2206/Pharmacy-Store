using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bill.Commands.Queries.GetAll
{
    public class GetAllBillsQueryHandler : IRequestHandler<GetAllBillsQuery, IEnumerable<Domain.Bill>>
    {
        private readonly IUnitOfWork _uow;

        public GetAllBillsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<Domain.Bill>> Handle(GetAllBillsQuery request, CancellationToken cancellationToken)
        {
            return await _uow.BillRepository.GetAllAsync();
        }
    }
}
