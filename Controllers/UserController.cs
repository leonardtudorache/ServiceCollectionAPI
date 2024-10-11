using Microsoft.AspNetCore.Mvc;
using ServiceCollectionAPI.Services.Interfaces.UserManagement;
using ServiceCollectionAPI.Controllers.RequestModels.Generic.Request.UserRequests;
using ServiceCollectionAPI.Exceptions;
using ServiceCollectionAPI.Services.Interfaces;
using ServiceCollectionAPI.Controllers.RequestModels.Generic;
using Microsoft.AspNetCore.Authorization;

namespace ServiceCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAuthorization")]
    [Route("api/[controller]")]
    [ApiController]
    //difference between controller and controllerBase???
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITenantContextService _tenantContextService;

        public UserController(IUserService userService, ITenantContextService tenantContextService)
        {
            _userService = userService;
            _tenantContextService = tenantContextService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserRequest user)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                await _userService.AddUser(user);

                return Ok();
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Register/Company")]
        public async Task<IActionResult> RegisterCompanyAsync([FromBody] CreateUserRequest user)
        {
            try
            {
                await _userService.AddUserWithNewTenant(user);

                return Ok();
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                var user = await _userService.GetUserByEmail(email);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsersAsync()
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                List<UserResponse> users = new List<UserResponse>();
                users = await _userService.GetAllUsers();

                if (users == null || users.Count == 0)
                    return NotFound("No users have been found");

                return Ok(users);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetByPhone/{phone}")]
        public async Task<IActionResult> GetUserByPhoneAsync(string phone)
        {
            try
            {
                //Not needed tenantId here
                var user = await _userService.GetUserByPhone(phone);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync(string userId)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                var user = await _userService.GetUserById(userId);

                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserRequest updateUserRequest)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                await _userService.UpdateUser(updateUserRequest);

                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Block/{userId}")]
        public async Task<IActionResult> BlockUserAsync(string userId)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                await _userService.BlockUser(userId);

                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Unblock/{userId}")]
        public async Task<IActionResult> UnblockUserAsync(string userId)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                await _userService.UnblockUser(userId);

                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Remove/{userId}")]
        public async Task<IActionResult> RemoveUserAsync(string userId)
        {
            try
            {
                #region SetTenantId
                HttpContext.Request.Headers.TryGetValue("tenantId", out var tenantId);
                if (tenantId.Count == 0)
                {
                    throw new TenantIdNotSetException("Tenant not set.");
                }
                _tenantContextService.SetTenantId(tenantId.First());
                #endregion

                await _userService.RemoveUser(userId);

                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return Conflict(ex.Message);
            }
            catch (TenantIdNotSetException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
