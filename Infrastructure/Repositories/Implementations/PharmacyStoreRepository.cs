using Domain;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public class PharmacyStoreRepository: GenericRepository<Domain.PharmacyStore>, IPharmacyStoreRepository
    {
        public PharmacyStoreRepository(Context context) : base(context)
        {
        }

        public override void Add(PharmacyStore entity)
        {
            if(string.IsNullOrWhiteSpace(entity.Name))
            {
                throw new ArgumentException("Pharmacy store name cannot be empty.");
            }
            base.Add(entity);
        }
    }
    
}
