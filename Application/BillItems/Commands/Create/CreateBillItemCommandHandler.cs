using Domain;
using MediatR;
using Infrastructure.UnitOfWork;
using Application.Bills.Commands.Create;
using ServiceStack.DataAnnotations;

public class CreateBillItemCommandHandler
    : IRequestHandler<CreateBillItemCommand, int>
{
    private readonly IUnitOfWork uow;

    public CreateBillItemCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<int> Handle(CreateBillItemCommand request, CancellationToken cancellationToken)
    {
        var bill = await uow.BillRepository.GetByIdWithItemsAsync(request.IdBill);

        if (bill == null)
            throw new Exception("Bill not found");

        var nextRb = await uow.BillItemRepository.GetNextRbAsync(request.IdBill);

        var item = new BillItem
        {
            IdBill = request.IdBill,
            Rb = nextRb,
            Price = request.Price,
            Description = request.Description,
            IdMedicine = request.IdMedicine 
        };

        bill.BillItems.Add(item);
        bill.TotalPrice = bill.BillItems.Sum(x => x.Price);

        await uow.SaveChangesAsync();

        return item.Rb;
    }
}



