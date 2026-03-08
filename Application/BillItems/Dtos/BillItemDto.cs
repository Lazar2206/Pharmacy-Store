using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BillItems.Dtos
{
    public class BillItemDto
    {
        public int Rb { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int IdMedicine { get; set; }
    }
}
