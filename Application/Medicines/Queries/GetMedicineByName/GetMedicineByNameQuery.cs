using Application.Medicines.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Medicines.Queries.GetMedicineByName
{
    public class GetMedicineByNameQuery : IRequest<IEnumerable<MedicineDto>>
    {
        public string Name { get; set; }
        public GetMedicineByNameQuery(string name) => Name = name;
    }
}
