using Qalam.MYSQL.Entities;
using Qalam.MYSQL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Qalam.Common.Helper;
using Qalam.BLL.ViewModels;
using System.Text.RegularExpressions;
using Qalam.Common.Enums;
using System.Net.Mail;
using Qalam.MYSQL.Repository;
using Qalam.Common.Extensions;
using Qalam.Common.Exceptions;
using System.IO;
using Qalam.BLL.Services;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Qalam.BLL.Managers
{
    public class UserManager : Repository<User>
    {
        private readonly IMapper _mapper;
        private readonly Lazy<TokenManager> _tokenManager;
        private readonly Lazy<MailService> _mailService;
        private readonly string APP_NAME;

        public UserManager(QalamDBContext dBContext, IMapper mapper, IConfiguration configuration, Lazy<TokenManager> tokenManager, Lazy<MailService> mailService) : base(dBContext)
        {
            _mapper = mapper;
            _tokenManager = tokenManager;
            _mailService = mailService;
            APP_NAME = configuration["AppSettings:Name"];
        }

        public async Task<Result<bool>> Registration(UserRegisterViewModel userViewModel)
        {
            try
            {
                var user = _mapper.Map<User>(userViewModel);

                // Check for role
                if (!Enum.IsDefined(typeof(EUserTypes), userViewModel.Type)
                || (userViewModel.Type == EUserTypes.Student && user.Student == null)
                || (userViewModel.Type == EUserTypes.Teacher && user.Teacher == null)
                || userViewModel.Password != userViewModel.RepeatPassword
                || !HelperFunctions.IsStrongPassword(userViewModel.Password))
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                // Add user role
                user.RoleId = (int)userViewModel.Type;
                user.Role = null;

                // Hash user password
                user.HashedPassword = Protected.CreatePasswordHash(userViewModel.Password);
                user.JoinDate = DateTime.UtcNow;
                user.IsActive = true;
                user.IsConfirmed = false;

                // Generate Referal code for user iff he is student
                if (user.Student != null)
                {
                    user.Student.Referalcode = Guid.NewGuid().ToString("N");
                }

                // Generate Stream key for user iff he is teacher
                if (user.Teacher != null)
                {
                    user.Teacher.StreamKey = Guid.NewGuid().ToString("N");
                }

                // check for free packages & referal by later

                // Add user
                var result = Add(user);

                if (result == null || !SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return await SendConfirmationEmail(result);
            }
            catch (DuplicateDataException e)
            {
                return ResultHelper.Failed<bool>(statusCode: EStatusCode.EmailExists, message: e.Message);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(message: e.Message);
            }
        }

        public async Task<Result<bool>> SendConfirmationEmail(User user, string userEmail = null)
        {
            try
            {
                // if user not pass as argument then select it from database
                if(user == null)
                {
                    user = Get(u => u.NormalizedEmail == userEmail.ToUpper(), "Role");
                }
                // if user still null then this user not exist
                if(user == null)
                {
                    throw new Exception(EStatusCode.UserNotExist.Description());
                }

                var token = _tokenManager.Value.GenerateUserToken(user, DateTime.Now.AddMinutes(60)).Token;

                var confirmLink = string.Format("{0}/account/confirm-account", RequestAttributes.AppBaseUrl);

                string body = File.ReadAllText("wwwroot/templates/registartion.html");
                body = body.Replace("{app-name}", APP_NAME);
                body = body.Replace("{link-path}", confirmLink);
                body = body.Replace("{user-name}", user.FullName);
                body = body.Replace("{user-email}", user.Email);
                body = body.Replace("{user-token}", token);


                await _mailService.Value.SendAsync(new MailMessageViewModel
                {
                    To = user.Email,
                    IsBodyHtml = true,
                    Body = body,
                    Subject = string.Format("Confirm your account on {0}", APP_NAME)
                });

                return ResultHelper.Succeeded(true, statusCode: EStatusCode.ConfirmationEmailSent);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Result<bool> ConfirmAccount(string email, string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                var user = Get(u => u.NormalizedEmail == email.ToUpper(), "Role");
                
                if (user == null)
                {
                    throw new Exception(EStatusCode.UserNotExist.Description());
                }

                if(user.IsConfirmed)
                {
                    throw new Exception(EStatusCode.AlreadyConfirmed.Description());
                }

                var userType = (EUserTypes)Enum.Parse(typeof(EUserTypes), user.Role.NormalizedName, true);
                string userEmail = _tokenManager.Value.ValidateToken(token, userType).UserEmail;
                
                if (userEmail != email)
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                user.IsConfirmed = true;
                Update(user, u => u.IsConfirmed);
                if(!SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(true);
            }
            catch (SecurityTokenExpiredException)
            {
                return ResultHelper.Failed<bool>(message: EStatusCode.TokenExpired.Description());
            }
            catch (ArgumentException)
            {
                return ResultHelper.Failed<bool>(message: EStatusCode.InvalidToken.Description());
            }
            catch (SecurityTokenValidationException)
            {
                return ResultHelper.Failed<bool>(message: EStatusCode.InvalidToken.Description());
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(message: e.Message);
            }
        }

        public Result<UserViewModel> Login(UserLoginViewModel user)
        {
            try
            {
                var userResult = Get(u => u.NormalizedEmail == user.Email.ToUpper(), "Teacher", "Student", "Role");

                if (userResult == null || 
                    !Protected.Validate(user.Password, userResult.HashedPassword))
                {
                    throw new Exception(EStatusCode.EmailOrPasswordWrong.Description());
                }
                if (!userResult.IsConfirmed)
                {
                    throw new Exception(EStatusCode.ProcessFailed.Description());
                }
                if (userResult.IsActive != true)
                {
                    throw new Exception(EStatusCode.ProcessFailed.Description());
                }

                return ResultHelper.Succeeded(_tokenManager.Value.GenerateUserToken(userResult, DateTime.UtcNow.AddYears(1)));
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<UserViewModel>(statusCode: EStatusCode.InvalidData, message: e.Message);
            }
        }

        public Result<bool> ChangePassword(UserPasswordViewModel userPassword)
        {
            try
            {
                if (!(userPassword.NewPassword == userPassword.RepeatedNewPassword) || !HelperFunctions.IsStrongPassword(userPassword.NewPassword))
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                var user = Get(u => u.NormalizedEmail == userPassword.UserEmail.ToUpper());
                if (user == null)
                {
                    throw new Exception(EStatusCode.UserNotExist.Description());
                }

                if (!Protected.Validate(userPassword.OldPassword, user.HashedPassword))
                {
                    throw new Exception(EStatusCode.WrongPassword.Description());
                }

                user.HashedPassword = Protected.CreatePasswordHash(userPassword.NewPassword);

                Update(user, u => u.HashedPassword);
                if (!SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(true);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(message: e.Message);
            }
        }

        public Result<bool> ResetPassword(UserPasswordViewModel userPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userPassword.ResetToken) || !(userPassword.NewPassword == userPassword.RepeatedNewPassword) || !HelperFunctions.IsStrongPassword(userPassword.NewPassword))
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                var user = Get(u => u.NormalizedEmail == userPassword.UserEmail.ToUpper(), "Role");

                if (user == null)
                {
                    throw new Exception(EStatusCode.UserNotExist.Description());
                }

                var userType = (EUserTypes)Enum.Parse(typeof(EUserTypes), user.Role.NormalizedName, true);
                string userEmail = _tokenManager.Value.ValidateToken(userPassword.ResetToken, userType).UserEmail;

                if (userEmail != userPassword.UserEmail)
                {
                    throw new Exception(EStatusCode.InvalidData.Description());
                }

                user.HashedPassword = Protected.CreatePasswordHash(userPassword.NewPassword);
                Update(user, u => u.HashedPassword);
                if (!SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }

                return ResultHelper.Succeeded(true);
            }
            catch (SecurityTokenDecryptionFailedException)
            {
                return ResultHelper.Failed<bool>(message: EStatusCode.InvalidToken.Description());
            }
            catch (SecurityTokenExpiredException)
            {
                return ResultHelper.Failed<bool>(message: EStatusCode.TokenExpired.Description());
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(message: e.Message);
            }
        }

        public async Task<Result<bool>> ForgotPassword(string userEmail)
        {
            try
            {
                var user = Get(u => u.NormalizedEmail == userEmail.ToUpper(), "Role");
                if (user == null)
                {
                    throw new Exception(EStatusCode.UserNotExist.Description());
                }

                var token = _tokenManager.Value.GenerateUserToken(user, DateTime.Now.AddMinutes(60)).Token;

                var confirmLink = string.Format("{0}/account/reset-password", RequestAttributes.AppBaseUrl);

                string body = File.ReadAllText("wwwroot/templates/forgot-password.html");
                body = body.Replace("{app-name}", APP_NAME);
                body = body.Replace("{link-path}", confirmLink);
                body = body.Replace("{user-name}", user.FullName);
                body = body.Replace("{user-email}", user.Email);
                body = body.Replace("{user-token}", token);


                await _mailService.Value.SendAsync(new MailMessageViewModel
                {
                    To = user.Email,
                    IsBodyHtml = true,
                    Body = body,
                    Subject = "Reset your account password"
                });

                return ResultHelper.Succeeded(true, statusCode: EStatusCode.ResetPasswordEmailSent);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(message: e.Message);
            }
        }

        public Result<bool> EditUserData(int userId, UserViewModel userData)
        {
            try
            {
                userData.Email = userData.UniqueKey = null;
                userData.CountryId = userData.EducationTypeId = 0;

                var user = Get(u => u.Id == userId, new string[] { "Student" });

                user.FullName = userData.FullName;
                user.PhoneNumber = userData.PhoneNumber;
                user.BirthDate = userData.BirthDate;
                user.About = userData.About;
                user.Facebook = userData.Facebook;
                if(user.Student != null)
                    user.Student.EducationYearId = userData.EducationYearId ?? user.Student.EducationYearId;
                
                Update(user);
                if (!SaveChanges())
                {
                    throw new Exception(EStatusCode.DatabaseError.Description());
                }
                
                return ResultHelper.Succeeded(data: true);
            }
            catch (Exception e)
            {
                return ResultHelper.Failed<bool>(statusCode: EStatusCode.ProcessFailed, message: e.Message);
            }
        }
    }
}
