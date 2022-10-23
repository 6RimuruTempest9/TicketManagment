using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagement.UserApi.BusinessLogic.Services;
using TicketManagement.UserApi.Models;

namespace TicketManagement.UserApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;

        public UserController(UserManager userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>List of all users contains in database.</returns>
        [HttpGet("getAll")]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var userEntities = await _userManager.IdentityUserManager.Users.ToListAsync();

            var users = new List<UserModel>();

            foreach (var userEntity in userEntities)
            {
                var user = new UserModel
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Email = userEntity.Email,
                    Balance = userEntity.Balance.ToString(),
                    Language = userEntity.Language,
                    TimeZone = userEntity.TimeZone,
                };

                users.Add(user);
            }

            return users;
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>User model.</returns>
        [HttpGet("getById/{id}")]
        public async Task<UserModel> GetUserById(string id)
        {
            var user = await _userManager.IdentityUserManager.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var userModel = new UserModel
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Balance = user.Balance.ToString(),
                Language = user.Language,
                TimeZone = user.TimeZone,
            };

            return userModel;
        }

        /// <summary>
        /// Update user state.
        /// </summary>
        /// <param name="userModel">User model.</param>
        /// <response code="200">User was successfully updated.</response>
        /// <response code="400">User was not found.</response>
        [HttpPost("update")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromForm] UserModel userModel)
        {
            var user = await _userManager.IdentityUserManager.FindByIdAsync(userModel.Id);

            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;
            user.Balance = decimal.Parse(userModel.Balance, CultureInfo.InvariantCulture);
            user.Language = userModel.Language;
            user.TimeZone = userModel.TimeZone;

            var result = await _userManager.IdentityUserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">User id.</param>
        /// /// <response code="200">User was successfully deleted.</response>
        /// /// <response code="400">User was not deleted.</response>
        [HttpGet("delete/{id}")]
        [Authorize(Roles = "Admin, Manager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.IdentityUserManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest();
            }

            await _userManager.IdentityUserManager.DeleteAsync(user);

            return Ok();
        }

        /// <summary>
        /// Change user password by id.
        /// </summary>
        /// <param name="model">Change password model.</param>
        /// <response code="200">User password was successfully updated.</response>
        /// <response code="400">Password changing is not complete.</response>
        [HttpPost("changePassword")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePasswordUserById([FromForm] ChangePasswordModel model)
        {
            var changePasswordResult =
                await _userManager.ChangePasswordUserByIdAsync(model.UserId, model.OldPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                return BadRequest(changePasswordResult.ValidationResult.Errors);
            }

            return Ok();
        }

        /// <summary>
        /// Get user roles by JWT.
        /// </summary>
        /// <param name="jwt">JWT.</param>
        /// <response code="200">User roles was found.</response>
        [HttpPost("getRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUserRolesByJwt([FromForm] string jwt)
        {
            var roles = _userManager.GetUserRolesByJwt(jwt);

            return Ok(roles);
        }

        /// <summary>
        /// Get user id by JWT.
        /// </summary>
        /// <param name="jwt">JWT.</param>
        /// <response code="200">User id was found.</response>
        [HttpPost("getIdByJwt")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIdByJwt([FromForm] string jwt)
        {
            var userId = await _userManager.GetIdByJwt(jwt);

            return Ok(userId);
        }

        /// <summary>
        /// Get user model by JWT.
        /// </summary>
        /// <param name="jwt">JWT.</param>
        /// <response code="200">User model was found.</response>
        [HttpPost("getUserByJwt")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByJwt([FromForm] string jwt)
        {
            var userId = await _userManager.GetIdByJwt(jwt);

            var user = await _userManager.IdentityUserManager.FindByIdAsync(userId);

            return Ok(user);
        }
    }
}