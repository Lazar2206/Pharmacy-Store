using Application.Patients.Dtos;
using Application.PharmacyStores.Dtos;
using Application.PharmacyStores.Queries.GetPharmacyStoreById;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Queries.GetPatientById
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto>
    {
        private readonly IUnitOfWork uow;

        public GetPatientByIdQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<PatientDto> Handle(
            GetPatientByIdQuery request,
            CancellationToken cancellationToken)
        {
            var patient = await uow.PatientRepository.GetByIdAsync(request.Id);

            if (patient == null)
                return null;

            return new PatientDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName
            };
        }
    }
}
