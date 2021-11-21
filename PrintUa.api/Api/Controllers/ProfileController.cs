using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Controllers.Base;
using Api.Models.OrderController;
using Business.Interface.Models;
using Business.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : BaseController
    {
        private readonly IProfileDataService _profileDataService;
        private readonly IProfileManager _profileManager;

        public ProfileController(IProfileDataService profileDataService, IProfileManager profileManager)
        {
            _profileDataService = profileDataService;
            _profileManager = profileManager;
        }

        #region Avatar

        //[HttpGet]
        //[Route("avatar")]
        //[Authorize]
        //public async Task<IActionResult> GetMyAvatar()
        //{
        //    return null;
        //}

        //[HttpPut]
        //[Route("avatar/update")]
        //[Authorize]
        //public async Task<IActionResult> UpdateMyAvatar([FromBody] AvatarModel avatar)
        //{
        //    return null;
        //}

        //[HttpDelete]
        //[Route("avatar/delete")]
        //[Authorize]
        //public async Task<IActionResult> DeleteMyAvatar()
        //{
        //    return null;
        //}

        #endregion

        #region Info data

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            List<UserInfoViewModel> usersInfoList = null;

            try
            {
                if (User.IsInRole("Admin"))
                {
                    usersInfoList = (List<UserInfoViewModel>)await _profileDataService.GetAllUsersInfo();
                }
                else
                {
                    throw new Exception("Invalid user role!");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok(usersInfoList);
        }

        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> GetMyInfo()
        {
            ProfileInfoModel profileInfo = null;

            try
            {
                if (User.IsInRole("Customer"))
                {
                    profileInfo = await _profileDataService.GetCustomerProfileInfoById(GetUserId());
                }
                else if (User.IsInRole("Employee"))
                {
                    profileInfo = await _profileDataService.GetEmployeeProfileInfoById(GetUserId());
                }
                else if (User.IsInRole("Admin"))
                {
                    profileInfo = await _profileDataService.GetAdminProfileInfoById(GetUserId());
                }
                else
                {
                    throw new Exception("Invalid user role!");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok(profileInfo);
        }

        [HttpPut]
        [Route("info/update")]
        public async Task<IActionResult> UpdateMyInfo([FromBody] ProfileInfoModel userInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model validation error!");
            }

            try
            {
                if (User.IsInRole("Customer"))
                {
                    await _profileDataService.UpdateCustomerProfileInfoById(userInfo, GetUserId());
                }
                else if (User.IsInRole("Employee"))
                {
                    await _profileDataService.UpdateEmployeeProfileInfoById(userInfo, GetUserId());
                }
                else if (User.IsInRole("Admin"))
                {
                    await _profileDataService.UpdateAdminProfileInfoById(userInfo, GetUserId());
                }
                else
                {
                    throw new Exception("Invalid user role!");
                }
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok();
        }

        #endregion

        #region Auth data

        #region Phone

        [HttpGet]
        [Route("phone")]
        public async Task<IActionResult> GetMyPhoneNumber()
        {
            string phone = null;

            try
            {
                phone = await _profileManager.GetPhoneNumberByUserId(GetUserId());
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok(phone);
        }

        [HttpPut]
        [Route("phone/update")]
        [Authorize]
        public async Task<IActionResult> UpdateMyEmail([FromBody] UpdatePhoneNumberModel phoneNumberModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model validation error!");
            }

            try
            {
                await _profileManager.UpdatePhoneNumberByUserId(phoneNumberModel, GetUserId());
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok();
        }

        #endregion

        #region Email

        [HttpGet]
        [Route("email")]
        public async Task<IActionResult> GetMyEmail()
        {
            string email = null;

            try
            {
                email = await _profileManager.GetEmailByUserId(GetUserId());
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok(email);
        }

        [HttpPut]
        [Route("email/update")]
        [Authorize]
        public async Task<IActionResult> UpdateMyEmail([FromBody] UpdateEmailModel emailModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model validation error!");
            }

            try
            {
                await _profileManager.UpdateEmailByUserId(emailModel, GetUserId());
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok();
        }

        #endregion

        #region Password

        [HttpPut]
        [Route("password/update")]
        public async Task<IActionResult> UpdateMyPassword([FromBody] UpdatePasswordModel passwordModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model validation error!");
            }

            try
            {
                await _profileManager.UpdatePasswordByUserId(passwordModel, GetUserId());
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

            return Ok();
        }

        #endregion

        #endregion
    }
}
