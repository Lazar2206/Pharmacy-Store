using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillItemsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public BillItemsController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        [HttpGet]
        public IActionResult GetAllBillItems()
        {
            var billItems = uow.BillItemRepository.GetAll();
            return Ok(billItems);
        }
    }
}
