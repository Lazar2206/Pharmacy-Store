using Application.Medicines.Commands.Create;
using Application.Medicines.Commands.Delete;
using Application.Medicines.Dtos;
using Application.Medicines.Queries.GetMedicineByName;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MedicinesController : ControllerBase
{
    private readonly IUnitOfWork uow;
    private readonly IMediator mediator;

    public MedicinesController(IUnitOfWork uow, IMediator mediator)
    {
        this.uow = uow;
        this.mediator = mediator;
    }

    
    [HttpGet("{id}")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var medicine = await uow.MedicineRepository.GetByIdAsync(id);

        if (medicine == null)
            return NotFound();

        return Ok(new
        {
            Id = medicine.IdMedicine,
            Name = medicine.Name,
            Price = medicine.Price
        });
    }

    
    [HttpPost("Create")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] CreateMedicineCommand command)
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
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteMedicineCommand(id));

        if (!result)
            return NotFound(new { message = "Lek nije pronađen." });

        return NoContent(); 
    }
    [HttpGet("search/{name}")]
    [Authorize(Roles = "user,admin")]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await mediator.Send(new GetMedicineByNameQuery(name));

        if (result == null)
            return NotFound(new { message = "Lek nije pronađen" });

        return Ok(result);
    }
}
