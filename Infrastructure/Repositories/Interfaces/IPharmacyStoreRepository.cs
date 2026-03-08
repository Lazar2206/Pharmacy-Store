using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPharmacyStoreRepository: IRepository<Domain.PharmacyStore>
    {
        Task<PharmacyStore?> GetByIdAsync(int id);
        void Add(PharmacyStore pharmacyStore);
        Task<IEnumerable<PharmacyStore>> GetAllAsync();
        Task<IEnumerable<PharmacyStore>> GetByNameAsync(string name);
    }
}
