using Application.Patients.Dtos;
using Application.PharmacyStores.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Queries.GetPatientById
{
    public class GetPatientByIdQuery : IRequest<PatientDto>
    {
        public int Id { get; }

        public GetPatientByIdQuery(int id)
        {
            Id = id;
        }
    }
}
