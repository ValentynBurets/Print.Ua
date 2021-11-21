using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Controllers.Base;
using Api.Models;
using Api.Services;
using AutoMapper;
using Business.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        private readonly UserManager<SystemUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IProfileRegistrationService _profileRegistrationService;

        public AuthenticationController(
            UserManager<SystemUser> userManager,
            IMapper mapper,
            IAuthManager authManager,
            IProfileRegistrationService profileRegistrationService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _authManager = authManager;
            _profileRegistrationService = profileRegistrationService;
        }

        [NonAction]
        private async Task<IActionResult> RegisterUser(RegisterUserModel userModel, string role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<SystemUser>(userModel);
                user.UserName = userModel.Email;
                
                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                await _userManager.AddToRoleAsync(user, role);

                await _profileRegistrationService.CreateProfile(user, userModel.FirstName, userModel.LastName);

                if (!await _authManager.ValidateUser(userModel))
                {
                    return Unauthorized();
                }

                return Accepted(new
                {
                    Token = await _authManager.CreateToken()
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterUserModel userModel)
        {
            return await RegisterUser(userModel, "Customer");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterUserModel userModel)
        {
            return await RegisterUser(userModel, "Employee");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserModel userModel)
        {
            return await RegisterUser(userModel, "Admin");
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(userModel))
                {
                    return Unauthorized();
                }

                return Accepted(new
                {
                    Token = await _authManager.CreateToken()
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.ToString());
            }
        }
    }
}
