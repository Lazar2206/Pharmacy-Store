using Domain;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly Context context;

        public PatientRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await context.Set<Patient>().FindAsync(id);
        }

        public void Add(Patient patient)
        {
            context.Set<Patient>().Add(patient);
        }

        public void Remove(Patient patient)
        {
            context.Set<Patient>().Remove(patient);
        }
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await context.Patients.ToListAsync();
        }
    }
}