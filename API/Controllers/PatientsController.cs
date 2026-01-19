using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController: ControllerBase
    {
        private readonly IUnitOfWork uow;
        public PatientsController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public IActionResult GetPatients()
        {
            var patients = uow.PatientRepository.GetAll();
            return Ok(patients);
        }
    }
}
