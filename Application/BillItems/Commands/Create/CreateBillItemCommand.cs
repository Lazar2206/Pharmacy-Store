using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bills.Commands.Create
{
    public class CreateBillItemCommand : IRequest<int>
    {
        public int IdBill { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int IdMedicine { get; set; }
    }
}
