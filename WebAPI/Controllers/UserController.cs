
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _IUserApplication;

        public UserController(IUserApplication IUserApplication)
        {
            _IUserApplication = IUserApplication;
        }

        [Produces("application/json")]
        [HttpPost("/api/CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
            {
                return Unauthorized();
            }

            var result = await _IUserApplication.CreateUser(login.email, login.password, login.age, login.cellphone);

            if (result)
            {
                return Ok("Success");
            }
            else
            {
                return Ok("Error to add user");
            }
        }
    }
}
