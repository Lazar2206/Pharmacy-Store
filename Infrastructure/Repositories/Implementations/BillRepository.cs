using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class BillRepository : GenericRepository<Domain.Bill>, Interfaces.IBillRepository
    {
        public BillRepository(Context context) : base(context)
        {
        }
    }
}
