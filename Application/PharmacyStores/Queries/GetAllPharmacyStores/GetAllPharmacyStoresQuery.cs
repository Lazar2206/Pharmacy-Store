using Application.PharmacyStores.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PharmacyStores.Queries.GetAllPharmacyStores
{
    public record GetAllPharmacyStoresQuery : IRequest<List<PharmacyStoreDto>>;
}
