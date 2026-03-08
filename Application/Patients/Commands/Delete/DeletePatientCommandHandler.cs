using Infrastructure.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Patients.Commands.Delete
{
    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, bool>
    {
        private readonly IUnitOfWork uow;
        public DeletePatientCommandHandler(IUnitOfWork uow) => this.uow = uow;

        public async Task<bool> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await uow.PatientRepository.GetByIdAsync(request.Id);
            if (patient == null) return false;

            uow.PatientRepository.Remove(patient);
            await uow.SaveChangesAsync();
            return true;
        }
    }
}
