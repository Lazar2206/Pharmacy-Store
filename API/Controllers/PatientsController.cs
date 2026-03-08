using Application.Medicines.Commands.Create;
using Application.Patients.Commands.Create;
using Application.Patients.Commands.Delete;
using Application.Patients.Dtos;
using Application.Patients.Queries.GetAllPatients;
using Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController: ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMediator mediator;
        public PatientsController(IUnitOfWork uow, IMediator mediator)
        {
            this.uow = uow;
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await uow.PatientRepository.GetByIdAsync(id);

            if (patient == null)
                return NotFound();

            return Ok(new
            {
                Id = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName
            });
        }

        [HttpPost("Create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreatePatientCommand command)
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
            var success = await mediator.Send(new DeletePatientCommand(id));
            if (!success) return NotFound();
            return NoContent(); 
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            
            var dtos = await mediator.Send(new GetAllPatientsQuery());

            return Ok(dtos);
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest("ID nije pronađen u tokenu.");

            var allPatients = await mediator.Send(new GetAllPatientsQuery());

            
            var patient = allPatients.FirstOrDefault(p =>
                p.IdentityUserId != null &&
                p.IdentityUserId.Trim().Equals(userId.Trim(), StringComparison.OrdinalIgnoreCase));

            if (patient == null)
                return NotFound($"Profil nije povezan. Traženi ID: {userId}");

            return Ok(patient);
        }
    }
}
