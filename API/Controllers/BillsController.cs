using Application.Bill.Commands.Create;
using Application.Bill.Commands.Queries;
using Application.Bill.Commands.Queries.GetAll;
using Application.Bill.Commands.Update;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMediator mediator;
        public BillsController(IUnitOfWork uow, IMediator mediator)
        {
            this.uow = uow;
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bill = await mediator.Send(new GetBillByIdQuery(id));

            if (bill == null)
                return NotFound();

            return Ok(bill);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(
        [FromBody] CreateBillCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newId = await mediator.Send(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newId },
                new { id = newId }
            );
        }
        [HttpPatch("UpdateStatus")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateBillStatusCommand command)
        {
            var result = await mediator.Send(command);

            if (!result)
                return BadRequest("Greška pri ažuriranju statusa ili račun ne postoji.");

            return Ok(new { message = "Status uspešno promenjen!" });
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bills = await mediator.Send(new GetAllBillsQuery());

            return Ok(bills);
        }
    }
}
