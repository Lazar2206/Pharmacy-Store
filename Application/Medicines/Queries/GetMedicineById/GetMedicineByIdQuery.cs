using Application.Medicines.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Medicines.Queries.GetMedicineById
{
    public class GetMedicineByIdQuery : IRequest<MedicineDto>
    {
        public int Id { get; }

        public GetMedicineByIdQuery(int id)
        {
            Id = id;
        }
    }
}
