using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Commands.Delete
{
    public class DeletePatientCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeletePatientCommand(int id) => Id = id;
    }
}
