using Domain;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations
{
    public class MedicineRepository
        : GenericRepository<Medicine>, IMedicineRepository
    {
        private readonly Context context;

        public MedicineRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<Medicine?> GetByIdAsync(int id)
        {
            return await context.Set<Medicine>().FindAsync(id);
        }

        public void Add(Medicine medicine)
        {
            context.Set<Medicine>().Add(medicine);
        }

        public void Remove(Medicine medicine)
        {
            context.Set<Medicine>().Remove(medicine);
        }
        public async Task<IEnumerable<Medicine>> GetByNameAsync(string name)
        {
            var searchTerm = name.ToLower();

            var terms = searchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var query = context.Medicines.AsQueryable();

            foreach (var term in terms)
            {
                query = query.Where(m =>
                    m.Name.ToLower().Contains(term) ||
                    (m.Tags != null && m.Tags.ToLower().Contains(term))
                );
            }

            return await query.ToListAsync();
        }
    }
}
