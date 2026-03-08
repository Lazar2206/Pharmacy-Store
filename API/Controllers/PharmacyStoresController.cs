using Application.PharmacyStores.Commands.Create;
using Application.PharmacyStores.Dtos;
using Application.PharmacyStores.Queries.GetAllPharmacyStores;
using Application.PharmacyStores.Queries.GetPharmacyStoreByName;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PharmacyStoresController : ControllerBase
{
    private readonly IUnitOfWork uow;
    private readonly IMediator mediator;

    public PharmacyStoresController(IUnitOfWork uow, IMediator mediator)
    {
        this.uow = uow;
        this.mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var store = await uow.PharmacyStoreRepository.GetByIdAsync(id);

        if (store == null)
            return NotFound();

        return Ok(new
        {
            Id = store.IdPharmacy,
            Name = store.Name
        });
    }

    [HttpPost("Create")]
    [AllowAnonymous]
    public async Task<IActionResult> Create(
        [FromBody] CreatePharmacyStoreCommand command)
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
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var dtos = await mediator.Send(new GetAllPharmacyStoresQuery());
        return Ok(dtos);
    }
    [HttpGet("search/{name}")]
    [Authorize]
    public async Task<IActionResult> GetByName(string name)
    {
        
        var dtos = await mediator.Send(new GetPharmacyStoreByNameQuery(name));

        return Ok(dtos);
    }
}