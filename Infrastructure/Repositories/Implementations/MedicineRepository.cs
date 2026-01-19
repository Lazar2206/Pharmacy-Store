using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class MedicineRepository : GenericRepository<Domain.Medicine>, Interfaces.IMedicineRepository
    {
        public MedicineRepository(Context context) : base(context)
        {
        }
    }
}
