using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qalam.BLL.Managers;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.Common.Extensions;
using Qalam.Common.Helper;
using Qalam.Configurations;

namespace Qalam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Lazy<UserManager> _manager;
        private readonly Lazy<RequestAttributes> _requestAttribute;

        public UserController(Lazy<UserManager> manager,
                              Lazy<RequestAttributes> requestAttribute)
        {
            _manager = manager;
            _requestAttribute = requestAttribute;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Registration")]
        public async Task<Result<bool>> AddUser([FromBody] UserRegisterViewModel user)
        {
            if (!ModelState.IsValid)
                return ResultHelper.Failed<bool>(statusCode: EStatusCode.InvalidData);

            return await _manager.Value.Registration(user);
        }

        [HttpPost]
        [Route("Login")]
        public Result<UserViewModel> Login([FromBody] UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
                return ResultHelper.Failed<UserViewModel>(message: EStatusCode.InvalidData.Description());

            return _manager.Value.Login(user);
        }

        [HttpPost]
        [Route("send-confirmation")]
        public async Task<Result<bool>> SendConfirmation([FromQuery] string userEmail)
        {
            return await _manager.Value.SendConfirmationEmail(null, userEmail);
        }

        [HttpGet]
        [Route("confirm-account")]
        public Result<bool> ConfirmAccount([FromQuery] string userEmail, [FromQuery] string token)
        {
            return _manager.Value.ConfirmAccount(userEmail, token);
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<Result<bool>> ForgotPassword([FromQuery] string userEmail)
        {
            return await _manager.Value.ForgotPassword(userEmail);
        }

        [HttpPost]
        [Route("reset-password")]
        public Result<bool> ResetPassword([FromBody] UserPasswordViewModel password)
        {
            if (!ModelState.IsValid)
                return ResultHelper.Failed<bool>(message: EStatusCode.InvalidData.Description());

            return _manager.Value.ResetPassword(password);
        }

        [HttpPost]
        [Route("change-password")]
        [Authorization(EUserTypes.Any)]
        public Result<bool> ChangePassword([FromBody] UserPasswordViewModel password)
        {
            if (!ModelState.IsValid)
                return ResultHelper.Failed<bool>(message: EStatusCode.InvalidData.Description());

            return _manager.Value.ChangePassword(password);
        }

        [HttpPut]
        [Authorization(EUserTypes.Admin, EUserTypes.Teacher, EUserTypes.Student)]
        public Result<bool> EditUserData([FromBody] UserViewModel userData)
        {
            return _manager.Value.EditUserData(_requestAttribute.Value.UserId, userData);
        }

    }
}