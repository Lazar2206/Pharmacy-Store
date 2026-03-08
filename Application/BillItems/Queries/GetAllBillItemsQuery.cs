using Application.BillItems.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BillItems.Queries
{
    public class GetAllBillItemsQuery : IRequest<List<BillItemDto>>
    {
    }
}
