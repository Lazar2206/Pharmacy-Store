using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IMedicineRepository: IRepository<Domain.Medicine>
    {
       
            Task<Medicine?> GetByIdAsync(int id);
            void Add(Medicine medicine);
            void Remove(Medicine medicine);
        Task<IEnumerable<Medicine>> GetByNameAsync(string name);
    }
}
