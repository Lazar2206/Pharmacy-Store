using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IBillItemRepository : IRepository<Domain.BillItem>
    {
        Task<List<BillItem>> GetAllAsync();
        Task<int> GetNextRbAsync(int billId);
    }
}
