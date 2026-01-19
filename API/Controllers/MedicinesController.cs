using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController: ControllerBase
    {
        private readonly IUnitOfWork uow;
        public MedicinesController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public IActionResult GetMedicines()
        {
            var medicines = uow.MedicineRepository.GetAll();
            return Ok(medicines);
        }

    }
}
