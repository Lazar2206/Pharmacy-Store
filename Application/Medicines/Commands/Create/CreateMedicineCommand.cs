using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Medicines.Commands.Create
{
    public class CreateMedicineCommand: IRequest<int>
    {
        public int IdMedicine { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Tags { get; set; }
    }
   
}
