using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Medicines.Commands.Delete
{
    public class DeleteMedicineCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteMedicineCommand(int id) => Id = id;
    }
}
