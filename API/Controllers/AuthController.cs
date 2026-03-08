using Application.Auth.Commands.Login;
using Application.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            var result = await mediator.Send(command);

            if (result == null)
            {
                return Unauthorized(new { message = "Pogrešan email ili lozinka!" });
            }
            return Ok(result);
        }
    }
}
