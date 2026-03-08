using Application.PharmacyStores.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PharmacyStores.Queries.GetPharmacyStoreByName
{
    public class GetPharmacyStoreByNameQuery : IRequest<IEnumerable<PharmacyStoreDto>>
    {
        public string Name { get; set; }
        public GetPharmacyStoreByNameQuery(string name) => Name = name;
    }
}
