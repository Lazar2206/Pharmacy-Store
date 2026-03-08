using Application.Patients.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Queries.GetAllPatients
{
    public record GetAllPatientsQuery : IRequest<List<PatientDto>>;
}
