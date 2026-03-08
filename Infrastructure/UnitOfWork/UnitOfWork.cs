using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context context;

        public UnitOfWork(Context context)
        {
            this.context = context;
            PharmacyStoreRepository = new PharmacyStoreRepository(context);
            PatientRepository = new PatientRepository(context);
            MedicineRepository = new MedicineRepository(context);
            BillItemRepository = new BillItemRepository(context);
            BillRepository = new BillRepository(context);
        }
        public IPharmacyStoreRepository PharmacyStoreRepository { get; set; }
        public IPatientRepository PatientRepository { get; set; }
        public IMedicineRepository MedicineRepository { get; set; }
        public IBillItemRepository BillItemRepository { get; set; }
        public IBillRepository BillRepository { get; set; }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
