using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController: ControllerBase
    {
        private readonly IUnitOfWork uow;
        public BillsController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public IActionResult GetBills()
        {
            var bills = uow.BillRepository.GetAll();
            return Ok(bills);
        }
    }
}
