using Domain;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class PharmacyStoreRepository
         : GenericRepository<PharmacyStore>, IPharmacyStoreRepository
    {
        private readonly Context context;

        public PharmacyStoreRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public async Task<PharmacyStore?> GetByIdAsync(int id)
        {
            return await context.Set<PharmacyStore>().FindAsync(id);
        }
        public async Task<IEnumerable<PharmacyStore>> GetAllAsync()
        {
            return await context.Set<PharmacyStore>().ToListAsync();
        }
        public async Task<IEnumerable<PharmacyStore>> GetByNameAsync(string name)
        {
            var searchTerm = name.ToLower();
            
            var terms = searchTerm.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var query = context.PharmacyStores.AsQueryable();

            foreach (var term in terms)
            {
             
                query = query.Where(ps => ps.Name.ToLower().Contains(term));
            }

            return await query.ToListAsync();
        }
    }

}
