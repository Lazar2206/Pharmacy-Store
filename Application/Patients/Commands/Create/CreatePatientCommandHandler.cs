using Application.PharmacyStores.Commands.Create;
using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Commands.Create
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
    {
        private readonly IUnitOfWork uow;

        public CreatePatientCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<int> Handle(
            CreatePatientCommand request,
            CancellationToken cancellationToken)
        {
            var patient = new Domain.Patient
            {
                FirstName = request.FirstName,
                LastName = request.LastName

            };

            uow.PatientRepository.Add(patient);
            await uow.SaveChangesAsync();

            return patient.IdPatient;
        }
    }
}
