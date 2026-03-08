using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBillRepository: IRepository<Domain.Bill>
    {
        Task<Bill> GetByIdAsync(int id);
        Task<Bill> GetByIdWithItemsAsync(int id);
        Task AddAsync(BillItem item);
        Task<IEnumerable<Bill>> GetAllAsync();



    }

}
