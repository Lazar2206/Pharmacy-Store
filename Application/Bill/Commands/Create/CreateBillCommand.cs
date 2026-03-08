using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bill.Commands.Create
{
   
        public class CreateBillCommand : IRequest<int>
        {
            public DateTime Date { get; set; }
            public decimal TotalPrice { get; set; }
            public int IdPharmacy { get; set; }
            public int IdPatient { get; set; }
        }
}
