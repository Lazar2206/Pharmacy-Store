using Application.Patients.Dtos;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Queries.GetAllPatients
{
    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, List<PatientDto>>
    {
        private readonly IUnitOfWork uow;

        public GetAllPatientsQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<List<PatientDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await uow.PatientRepository.GetAllAsync();

            return patients.Select(p => new PatientDto
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                IdentityUserId = p.IdentityUserId

            }).ToList();
        }
    }
}
