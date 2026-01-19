using Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IPharmacyStoreRepository PharmacyRepository { get; set; }
        public IPatientRepository PatientRepository { get; set; }
        public IMedicineRepository MedicineRepository { get; set; }
        public IBillItemRepository BillItemRepository { get; set; }
        public IBillRepository BillRepository { get; set; }
        void SaveChanges(); 

    }
}
