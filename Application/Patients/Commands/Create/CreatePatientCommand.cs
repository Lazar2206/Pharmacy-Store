using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Commands.Create
{
    public class CreatePatientCommand: IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
