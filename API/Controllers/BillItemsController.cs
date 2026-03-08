using Application.Bills.Commands.Create;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillItemsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMediator mediator;

        public BillItemsController(IUnitOfWork uow, IMediator mediator)
        {
            this.uow = uow;
            this.mediator = mediator;
        }
        [HttpGet]
        public IActionResult GetAllBillItems()
        {
            var billItems = uow.BillItemRepository.GetAll();
            return Ok(billItems);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBillItem(CreateBillItemCommand command)
        {
            var rb = await mediator.Send(command);
            return Ok(new { Rb = rb });
        }
    }
}
