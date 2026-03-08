using Application.BillItems.Dtos;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bill.Commands.Queries
{
    public class BillDto
    {
        public int IdBill { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int IdPharmacy { get; set; }
        public int IdPatient { get; set; }

        public List<BillItemDto> BillItems { get; set; } = new();
    }
}
