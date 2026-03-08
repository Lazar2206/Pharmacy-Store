using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PharmacyStores.Dtos
{
    public class PharmacyStoreDto
    {
        public int IdPharmacy { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; } 
        public string Longitude { get; set; } 
    }
}
