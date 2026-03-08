using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bill.Commands.Queries
{
    public class GetBillByIdQuery : IRequest<BillDto>
    {
        public int Id { get; }

        public GetBillByIdQuery(int id)
        {
            Id = id;
        }
    }
}
