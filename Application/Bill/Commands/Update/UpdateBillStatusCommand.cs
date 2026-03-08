using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bill.Commands.Update
{
    public class UpdateBillStatusCommand : IRequest<bool>
    {
        public int IdBill { get; set; }
        public string NewStatus { get; set; }
    }
}
