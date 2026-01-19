using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyStoresController: ControllerBase
    {
        private readonly IUnitOfWork uow;
        public PharmacyStoresController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        [HttpGet]
        public IActionResult GetPharmacyStores()
        {
            var pharmacyStores = uow.PharmacyRepository.GetAll();
            return Ok(pharmacyStores);
        }

    }
}
