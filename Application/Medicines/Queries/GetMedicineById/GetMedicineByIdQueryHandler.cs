using MediatR;
using Infrastructure.UnitOfWork;
using Application.Medicines.Queries.GetMedicineById;
using Application.Medicines.Dtos;

public class GetMedicineByIdQueryHandler
    : IRequestHandler<GetMedicineByIdQuery, MedicineDto>
{
    private readonly IUnitOfWork uow;

    public GetMedicineByIdQueryHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<MedicineDto> Handle(
        GetMedicineByIdQuery request,
        CancellationToken cancellationToken)
    {
        var medicine = await uow.MedicineRepository
            .GetByIdAsync(request.Id);

        if (medicine == null)
            return null; 

        return new MedicineDto
        {
            IdMedicine = medicine.IdMedicine,
            Name = medicine.Name,
            Price = medicine.Price
        };
    }
}