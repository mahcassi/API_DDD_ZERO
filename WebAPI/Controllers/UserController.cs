
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Token;
using Microsoft.AspNetCore.Identity;
using Entities.Entities;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Entities.Enums;

namespace WebAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _IUserApplication;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserController(IUserApplication IUserApplication, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _IUserApplication = IUserApplication;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
            {
                return Unauthorized();
            }

            var result = await _IUserApplication.ExistUser(login.email, login.password);

            if (result)
            {
                var idUser = await _IUserApplication.ReturnUserId(login.email);

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                .AddSubject("teste")
                .AddIssuer("Teste.Securiry.Bearer")
                .AddAudience("Teste.Securiry.Bearer")
                .AddClaim("idUser", idUser)
                .AddExpiry(5)
                .Builder();

                return Ok(token.value);

            } else {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateToken")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
            {
                return Unauthorized();
            }

            var result = await _signInManager.PasswordSignInAsync(login.email, login.password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var idUser = await _IUserApplication.ReturnUserId(login.email);

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                .AddSubject("teste")
                .AddIssuer("Teste.Securiry.Bearer")
                .AddAudience("Teste.Securiry.Bearer")
                .AddClaim("idUser", idUser)
                .AddExpiry(5)
                .Builder();

                return Ok(token.value);

            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AdicionaUsuarioIdentity")]
        public async Task<IActionResult> CreateUserIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.email) || string.IsNullOrWhiteSpace(login.password))
            {
                return Unauthorized();
            }

            var user = new ApplicationUser
            {
                UserName = login.email,
                Email = login.email,
                Cellphone = login.cellphone,
                UserType = EUserType.Common
            };

            //password encryption
            var resultado = await _userManager.CreateAsync(user, login.password);

            if (resultado.Errors.Any())
            {
                return Ok(resultado.Errors);
            }

            // Gernerate confirmation e-mail 
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // return email
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var result2 = await _userManager.ConfirmEmailAsync(user, code);

            if (result2.Succeeded)
            {
                return Ok("Usuário Adicionado com Sucesso!");
            }
            else
            {
                return Ok("Falha ao confirmar usuário!");
            }
        }
    }
}
