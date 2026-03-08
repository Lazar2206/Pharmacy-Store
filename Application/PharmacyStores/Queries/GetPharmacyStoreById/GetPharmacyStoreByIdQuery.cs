using Application.PharmacyStores.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PharmacyStores.Queries.GetPharmacyStoreById
{
    public class GetPharmacyStoreByIdQuery : IRequest<PharmacyStoreDto>
    {
        public int Id { get; }

        public GetPharmacyStoreByIdQuery(int id)
        {
            Id = id;
        }
    }
}
