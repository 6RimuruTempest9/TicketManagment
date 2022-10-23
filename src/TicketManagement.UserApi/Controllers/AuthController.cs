using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.UserApi.Attributes.Authorization;
using TicketManagement.UserApi.BusinessLogic.Dto;
using TicketManagement.UserApi.BusinessLogic.Services;
using TicketManagement.UserApi.Models;

namespace TicketManagement.UserApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager _userManager;

        public AuthController(UserManager userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Register.
        /// </summary>
        /// <param name="registrationModel">Registration model.</param>
        /// <response code="200">Registration is success.</response>
        /// <response code="400">Registration is not success.</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromForm] RegistrationModel registrationModel)
        {
            var registrationDto = new RegistrationDto
            {
                Email = registrationModel.Email,
                Password = registrationModel.Password,
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                Language = registrationModel.Language,
                TimeZone = registrationModel.TimeZone,
            };

            var result = await _userManager.Register(registrationDto);

            if (!result.Succeeded)
            {
                return BadRequest(result.ValidationResult.Errors);
            }

            return Ok(result.Jwt);
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="loginModel">Login model.</param>
        /// <response code="200">User is login.</response>
        /// <response code="403">User is not login.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            var loginDto = new LoginDto
            {
                Email = loginModel.Email,
                Password = loginModel.Password,
            };

            var result = await _userManager.Login(loginDto);

            if (!result.Succeeded)
            {
                return Forbid();
            }

            return Ok(result.Jwt);
        }

        /// <summary>
        /// Check valid of JWT.
        /// </summary>
        /// <param name="jwt">JWT.</param>
        /// <response code="200">JWT is valid.</response>
        /// <response code="400">JWT is not valid.</response>
        [HttpPost("jwtValidation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult JwtValidation([FromForm] string jwt)
        {
            var isJwtValid = _userManager.IsJwtValid(jwt);

            if (!isJwtValid)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}